using BlockTypes;
using Jobs;
using NPC;
using Recipes;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class OliveFarmerAreaJob : AbstractFarmAreaJobDefinition
	{
		public string NPCTypeString { get; protected set; }
		public float Cooldown { get; set; } = 6.8f;

		public OliveFarmerAreaJob ()
		{
			Identifier = NPCTypeString = "pipliz.olivefarmer";
			UsedNPCType = NPCType.GetByKeyNameOrDefault(NPCTypeString);
			MaxGathersPerRun = 5;
			Stages = new ushort[] {
				ItemTypes.IndexLookup.GetIndex("olivesapling"),
				ItemTypes.IndexLookup.GetIndex("logtemperate")
			};
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			return new OliveFarmerJob(this, owner, min, max, npcID);
		}

		public class OliveFarmerJob : AbstractAreaJob
		{
			// store treeLocation separately from positionSub because the farmer will move next to these positions(they're not equal)
			protected Vector3Int treeLocation = Vector3Int.invalidPos;

			static readonly ItemTypes.ItemType[] yTypesBuffer = new ItemTypes.ItemType[5]; // max 3 Y + 1 below + 1 above
			static readonly List<Vector3Int> treePositions = new List<Vector3Int>();
			static readonly List<RecipeResult> GatherResults = new List<RecipeResult>();

			public int GatheredCount { get; set; }

			public OliveFarmerJob (AbstractAreaJobDefinition def, Colony owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(def, owner, min, max, npcID) { }

			public override void CalculateSubPosition ()
			{
				ThreadManager.AssertIsMainThread();
				Vector3Int min = Minimum;
				Vector3Int max = Maximum;
				int ySize = max.y - min.y + 1;

				treePositions.Clear();
				for (int x = min.x + 1; x < max.x; x += 3) {
					for (int z = min.z + 1; z < max.z; z += 3) {
						treePositions.Add(new Vector3Int(x, 0, z));
					}
				}

				while (treePositions.Count > 0) {
					int idx = Random.Next(0, treePositions.Count);
					Vector3Int pos = treePositions[idx];
					treePositions.RemoveAt(idx);

					if (!World.TryGetColumn(new Vector3Int(pos.x, min.y - 1, pos.z), ySize + 2, yTypesBuffer)) {
						goto DUMB_RANDOM;
					}

					for (int y = 0; y < ySize; y++) {
						ItemTypes.ItemType typeBelow = yTypesBuffer[y];
						ItemTypes.ItemType type = yTypesBuffer[y + 1];
						ItemTypes.ItemType typeAbove = yTypesBuffer[y + 2];

						if (
							(type == BuiltinBlocks.Types.air || type == BuiltinBlocks.Types.logtemperate || type == BuiltinBlocks.Types.olivesapling)
							&& (typeAbove == BuiltinBlocks.Types.air || typeAbove == BuiltinBlocks.Types.logtemperate)
							&& typeBelow.IsFertile
						) {
							treeLocation = new Vector3Int(pos.x, min.y + y, pos.z);
							if (AI.PathingManager.TryGetClosestPositionWorldNotAt(treeLocation, NPC.Position, out bool canStand, out Vector3Int position)) {
								if (canStand) {
									positionSub = position;
									return;
								}
							}
						}
					}
				}

				// none of the spots were viable tree locations (???)

				DUMB_RANDOM:
				treeLocation = Vector3Int.invalidPos;
				positionSub = min.Add(
					Random.Next(0, (max.x - min.x) / 3) * 3,
					(max.y - min.y) / 2,
					Random.Next(0, (max.z - min.z) / 3) * 3
				);
			}

			public override void OnNPCAtJob (ref NPCBase.NPCState state)
			{
				OliveFarmerAreaJob def = (OliveFarmerAreaJob)definition;

				ThreadManager.AssertIsMainThread();
				state.JobIsDone = true;
				positionSub = Vector3Int.invalidPos;
				if (!treeLocation.IsValid) { // probably idling about
					state.SetCooldown(Random.NextFloat(8f, 16f));
					return;
				}

				if (!World.TryGetTypeAt(treeLocation, out ushort type)) {
					state.SetCooldown(10.0);
					return;
				}

				if (type == BuiltinBlocks.Indices.logtemperate) { // gather olives
					var recipes = Owner.RecipeData.GetAvailableRecipes(def.NPCTypeString);
					Recipe.RecipeMatch match = Recipe.MatchRecipe(recipes, Owner);
					switch (match.MatchType) {
						case Recipe.RecipeMatchType.AllDone:
						case Recipe.RecipeMatchType.FoundMissingRequirements:
							if (state.Inventory.IsEmpty) {
								state.JobIsDone = true;
								if (match.MatchType == Recipe.RecipeMatchType.AllDone) {
									state.SetIndicator(new Shared.IndicatorState(def.Cooldown, BuiltinBlocks.Indices.erroridle));
								} else {
									state.SetIndicator(new Shared.IndicatorState(def.Cooldown, match.FoundRecipe.FindMissingType(Owner.Stockpile), true, false));
								}
							} else {
								shouldDumpInventory = true;
							}
							return;
						case Recipe.RecipeMatchType.FoundCraftable:
							var recipe = match.FoundRecipe;
							if (Owner.Stockpile.TryRemove(recipe.Requirements)) {
								// should always succeed, as the match above was succesfull
								GatherResults.Clear();
								for (int i = 0; i < recipe.Results.Count; i++) {
									GatherResults.Add(recipe.Results[i]);
								}

								ModLoader.Callbacks.OnNPCCraftedRecipe.Invoke(this, recipe, GatherResults);

								RecipeResult toShow = RecipeResult.GetWeightedRandom(GatherResults);
								if (toShow.Amount > 0) {
									state.SetIndicator(new Shared.IndicatorState(def.Cooldown, toShow.Type));
								} else {
									state.SetCooldown(def.Cooldown);
								}

								NPC.Inventory.Add(GatherResults);

								GatheredCount++;
								if (GatheredCount >= def.MaxGathersPerRun) {
									GatheredCount = 0;
									shouldDumpInventory = true;
								}
							}
							return;
					}
				} else if (type == 0) { // maybe plant sapling?
					if (World.TryGetTypeAt(treeLocation.Add(0, -1, 0), out ItemTypes.ItemType typeBelow)) {
						if (typeBelow.IsFertile) {
							ServerManager.TryChangeBlock(treeLocation, BuiltinBlocks.Types.air, BuiltinBlocks.Types.olivesapling, Owner, ESetBlockFlags.DefaultAudio);
							state.SetCooldown(2.0);
							return;
						}
					} else {
						state.SetCooldown(10.0);
						return;
					}
				} else {
					// very likely it's a sapling, so idle about
				}

				// something unexpected or idling
				state.SetCooldown(Random.NextFloat(4f, 8f));
				return;
			}
		}
	}
}
