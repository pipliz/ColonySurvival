using BlockTypes;
using NPC;
using Jobs;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class BerryFarmerDefinition : AbstractAreaJobDefinition<BerryFarmerDefinition>
	{
		public BerryFarmerDefinition ()
		{
			Identifier = "pipliz.berryfarm";
			fileName = "berryfarms";
			UsedNPCType = NPCType.GetByKeyNameOrDefault("pipliz.berryfarmer");
			AreaType = Shared.EAreaType.BerryFarm;
		}

		/// Override it to use custom berryfarmerjob; required for custom bush location logic
		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			return new BerryFarmerJob(owner, min, max, npcID);
		}

		public class BerryFarmerJob : AbstractAreaJob<BerryFarmerDefinition>
		{
			// store bushlocation separately from positionSub because the berry farmer will move next to bushes (they're not equal)
			protected Vector3Int bushLocation = Vector3Int.invalidPos;
			protected bool checkMissingBushes = true;
			static ItemTypes.ItemType[] yTypesBuffer = new ItemTypes.ItemType[5]; // max 3 Y + 1 below + 1 above
			public static float HarvestCooldown = 8.5f;

			public BerryFarmerJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(owner, min, max, npcID) { }

			public override void CalculateSubPosition ()
			{
				ThreadManager.AssertIsMainThread();

				Vector3Int min = Minimum;
				Vector3Int max = Maximum;
				int ySize = max.y - min.y + 1;
				if (checkMissingBushes && NPC.Colony.Stockpile.Contains(BuiltinBlocks.BerryBush)) {
					for (int x = min.x; x <= max.x; x += 2) {
						for (int z = min.z; z <= max.z; z += 2) {
							for (int y = -1; y <= ySize; y++) {
								if (!World.TryGetTypeAt(new Vector3Int(x, min.y + y, z), out yTypesBuffer[y + 1])) {
									goto DUMB_RANDOM;
								}
							}

							for (int y = 0; y < ySize; y++) {
								ItemTypes.ItemType typeBelow = yTypesBuffer[y];
								ItemTypes.ItemType type = yTypesBuffer[y + 1];
								ItemTypes.ItemType typeAbove = yTypesBuffer[y + 2];

								if (typeAbove.BlocksPathing || !typeBelow.IsFertile) {
									continue; // check next Y layer
								}
								Vector3Int pos = new Vector3Int(x, min.y + y, z);

								if (type == ItemTypes.Air) {
									bushLocation = pos;
									positionSub = AI.AIManager.ClosestPositionNotAt(bushLocation, NPC.Position);
									return;
								}
							}
						}
					}
					checkMissingBushes = false;
				}

				for (int i = 0; i < 5; i++) {
					// give the random positioning 5 chances to become valid
					Vector3Int test = min.Add(
						Random.Next(0, (max.x - min.x) / 2 + 1) * 2,
						0,
						Random.Next(0, (max.z - min.z) / 2 + 1) * 2
					);

					for (int y = -1; y <= ySize; y++) {
						if (!World.TryGetTypeAt(test.Add(0, y, 0), out yTypesBuffer[y + 1])) {
							goto DUMB_RANDOM;
						}
					}

					for (int y = 0; y < ySize; y++) {
						ItemTypes.ItemType typeBelow = yTypesBuffer[y];
						ItemTypes.ItemType type = yTypesBuffer[y + 1];
						ItemTypes.ItemType typeAbove = yTypesBuffer[y + 2];

						if (typeAbove.BlocksPathing || !typeBelow.IsFertile) {
							continue; // check next Y layer
						}
						if (type.ItemIndex == BuiltinBlocks.BerryBush) {
							positionSub = test.Add(0, y, 0);
							return;
						}

						if (type.ItemIndex == 0) {
							checkMissingBushes = true;
							bushLocation = test.Add(0, y, 0);
							positionSub = AI.AIManager.ClosestPositionNotAt(bushLocation, NPC.Position);
							return;
						}
					}
				}

				DUMB_RANDOM:
				positionSub = min.Add(
					Random.Next(0, (max.x - min.x) / 2 + 1) * 2,
					(max.x - min.x) / 2,
					Random.Next(0, (max.z - min.z) / 2 + 1) * 2
				);
			}

			static System.Collections.Generic.List<ItemTypes.ItemTypeDrops> GatherResults = new System.Collections.Generic.List<ItemTypes.ItemTypeDrops>();

			public override void OnNPCAtJob (ref NPCBase.NPCState state)
			{
				ThreadManager.AssertIsMainThread();
				state.JobIsDone = true;
				if (!positionSub.IsValid) {
					// likely moving in unloaded chunks
					state.SetCooldown(10.0);
					return;
				}

				Vector3Int pos = bushLocation.IsValid ? bushLocation : positionSub;
				positionSub = Vector3Int.invalidPos; // mark position invalid to force sub location recalculation after this job

				ushort type;
				if (!World.TryGetTypeAt(pos, out type)) {
					state.SetCooldown(10.0);
					return;
				}

				if (type == BuiltinBlocks.BerryBush) {
					// at bush, harvesting
					GatherResults.Clear();
					GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.Berry, 1, 1.0));
					GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.BerryBush, 1, 0.1));

					ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, this as IJob, pos, GatherResults);

					InventoryItem toShow = ItemTypes.ItemTypeDrops.GetWeightedRandom(GatherResults);
					if (toShow.Amount > 0) {
						state.SetIndicator(new Shared.IndicatorState(HarvestCooldown, toShow.Type));
					} else {
						state.SetCooldown(HarvestCooldown);
					}

					NPC.Inventory.Add(GatherResults);
					return;
				}


				if (type == 0 && pos >= Minimum && pos <= Maximum) {
					if (World.TryGetTypeAt(pos.Add(0, -1, 0), out ItemTypes.ItemType typeBelow)) {
						if (typeBelow.IsFertile) {
							if (NPC.Colony.Stockpile.TryRemove(BuiltinBlocks.BerryBush)) {
								ServerManager.TryChangeBlock(pos, 0, BuiltinBlocks.BerryBush, Owner, ESetBlockFlags.DefaultAudio);
								state.SetCooldown(2.0);
							} else {
								state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(8f, 14f), BuiltinBlocks.BerryBush, true, false));
							}
							return;
						}
					} else {
						state.SetCooldown(10.0);
						return;
					}
				}
				// ? nothing to do at this position
				state.SetCooldown(Random.NextFloat(3f, 6f));
			}
		}
	}
}
