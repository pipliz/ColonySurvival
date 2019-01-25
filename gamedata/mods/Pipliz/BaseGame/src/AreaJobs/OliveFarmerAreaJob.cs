using BlockTypes;
using Jobs;
using NPC;
using Recipes;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class OliveFarmerAreaJob : AbstractFarmAreaJobDefinition
	{
		public string NPCTypeString { get; protected set; }
		public float Cooldown { get; set; } = 8.5f;

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
			static ItemTypes.ItemType[] yTypesBuffer = new ItemTypes.ItemType[5]; // max 3 Y + 1 below + 1 above
			public int GatheredCount { get; set; }

			public OliveFarmerJob (AbstractAreaJobDefinition def, Colony owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(def, owner, min, max, npcID) { }

			public override void CalculateSubPosition ()
			{
				ThreadManager.AssertIsMainThread();
				bool hasSeeds = NPC.Colony.Stockpile.Contains(BuiltinBlocks.Sapling);
				Vector3Int min = Minimum;
				Vector3Int max = Maximum;
				int ySize = max.y - min.y + 1;

				for (int x = min.x + 1; x < max.x; x += 3) {
					for (int z = min.z + 1; z < max.z; z += 3) {
						if (!World.TryGetColumn(new Vector3Int(x, min.y - 1, z), ySize + 2, yTypesBuffer)) {
							goto DUMB_RANDOM;
						}

						for (int y = 0; y < ySize; y++) {
							ItemTypes.ItemType typeBelow = yTypesBuffer[y];
							ItemTypes.ItemType type = yTypesBuffer[y + 1];
							ItemTypes.ItemType typeAbove = yTypesBuffer[y + 2];

							if (
								((type == ItemTypes.Air && hasSeeds) || type.ItemIndex == BuiltinBlocks.LogTemperate)
								&& (typeAbove == ItemTypes.Air || typeAbove.ItemIndex == BuiltinBlocks.LogTemperate)
								&& typeBelow.IsFertile
							) {
								treeLocation = new Vector3Int(x, min.y + y, z);
								positionSub = AI.AIManager.ClosestPositionNotAt(treeLocation, NPC.Position);
								return;
							}
						}
					}
				}

				for (int i = 0; i < 15; i++) {
					// give the random positioning 5 chances to become valid
					int testX = min.x + Random.Next(0, (max.x - min.x - 2) / 3 + 1) * 3;
					int testZ = min.z + Random.Next(0, (max.z - min.z - 2) / 3 + 1) * 3;

					if (!World.TryGetColumn(new Vector3Int(testX, min.y - 1, testZ), ySize + 2, yTypesBuffer)) {
						goto DUMB_RANDOM;
					}

					for (int y = 0; y < ySize; y++) {
						ItemTypes.ItemType typeBelow = yTypesBuffer[y];
						ItemTypes.ItemType type = yTypesBuffer[y + 1];
						ItemTypes.ItemType typeAbove = yTypesBuffer[y + 2];

						if (typeBelow.BlocksPathing && !type.BlocksPathing && !typeAbove.BlocksPathing) {
							positionSub = new Vector3Int(testX, min.y + y, testZ);
							treeLocation = Vector3Int.invalidPos;
							return;
						}
					}
				}

				DUMB_RANDOM:
				treeLocation = Vector3Int.invalidPos;
				positionSub = min.Add(
					Random.Next(0, (max.x - min.x) / 3) * 3,
					(max.y - min.y) / 2,
					Random.Next(0, (max.z - min.z) / 3) * 3
				);
			}

			static System.Collections.Generic.List<ItemTypes.ItemTypeDrops> GatherResults = new System.Collections.Generic.List<ItemTypes.ItemTypeDrops>();

			public override void OnNPCAtJob (ref NPCBase.NPCState state)
			{
				OliveFarmerAreaJob def = (OliveFarmerAreaJob)definition;
				ItemTypes.ItemType saplingType = ItemTypes.GetType("olivesapling");

				ThreadManager.AssertIsMainThread();
				state.JobIsDone = true;
				positionSub = Vector3Int.invalidPos;
				Vector3Int min = Minimum;
				Vector3Int max = Maximum;
				if (!treeLocation.IsValid) { // probably idling about
					state.SetCooldown(Random.NextFloat(8f, 16f));
					return;
				}

				ushort type;
				if (!World.TryGetTypeAt(treeLocation, out type)) {
					state.SetCooldown(10.0);
					return;
				}

				if (type == BuiltinBlocks.LogTemperate) {
					var recipes = Owner.RecipeData.GetAvailableRecipes(def.NPCTypeString);
					Recipe.RecipeMatch match = Recipe.MatchRecipe(recipes, Owner);
					switch (match.MatchType) {
						case Recipe.RecipeMatchType.AllDone:
						case Recipe.RecipeMatchType.FoundMissingRequirements:
							if (state.Inventory.IsEmpty) {
								state.JobIsDone = true;
								if (match.MatchType == Recipe.RecipeMatchType.AllDone) {
									state.SetIndicator(new Shared.IndicatorState(def.Cooldown, BuiltinBlocks.ErrorIdle));
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

								ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, this as IJob, treeLocation, GatherResults);

								InventoryItem toShow = ItemTypes.ItemTypeDrops.GetWeightedRandom(GatherResults);
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
				} else if (type == 0) {
					// maybe plant sapling?
					if (World.TryGetTypeAt(treeLocation.Add(0, -1, 0), out ItemTypes.ItemType typeBelow)) {
						if (typeBelow.IsFertile) {
							if (Owner.Stockpile.TryRemove(saplingType.ItemIndex)) {
								ServerManager.TryChangeBlock(treeLocation, ItemTypes.Air, saplingType, Owner, ESetBlockFlags.DefaultAudio);
								state.SetCooldown(2.0);
								return;
							} else {
								state.SetIndicator(new Shared.IndicatorState(6f, saplingType.ItemIndex, true, false));
								return;
							}
						}
					} else {
						state.SetCooldown(10.0);
						return;
					}
				}

				// something unexpected
				state.SetCooldown(Random.NextFloat(8f, 16f));
				return;
			}
		}
	}
}
