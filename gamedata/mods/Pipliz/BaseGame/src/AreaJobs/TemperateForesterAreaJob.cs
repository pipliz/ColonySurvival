using BlockTypes;
using Jobs;
using NPC;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class TemperateForesterDefinition : AbstractFarmAreaJobDefinition
	{
		public TemperateForesterDefinition ()
		{
			Identifier = "pipliz.temperateforest";
			UsedNPCType = NPCType.GetByKeyNameOrDefault("pipliz.forester");
			MaxGathersPerRun = 1;
			Stages = new ushort[] {
				ItemTypes.IndexLookup.GetIndex("sappling"),
				ItemTypes.IndexLookup.GetIndex("logtemperate")
			};
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			return new ForesterJob(this, owner, min, max, npcID);
		}

		static bool ChopTree (Vector3Int p, BlockChangeRequestOrigin origin)
		{
			for (int y = 0; y < 5; y++) {
				switch (ServerManager.TryChangeBlock(p.Add(0, y, 0), BuiltinBlocks.Types.logtemperate, BuiltinBlocks.Types.air, origin)) {
					case EServerChangeBlockResult.CancelledByCallback:
					case EServerChangeBlockResult.ChunkNotReady:
					default:
						return false;
					case EServerChangeBlockResult.Success:
						break;
					case EServerChangeBlockResult.UnexpectedOldType:
						y = 15;
						break;
				}
			}
			return true;
		}

		public class ForesterJob : AbstractAreaJob
		{
			// store treeLocation separately from positionSub because the farmer will move next to these positions(they're not equal)
			protected Vector3Int treeLocation = Vector3Int.invalidPos;
			static ItemTypes.ItemType[] yTypesBuffer = new ItemTypes.ItemType[5]; // max 3 Y + 1 below + 1 above

			public ForesterJob (AbstractAreaJobDefinition def, Colony owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(def, owner, min, max, npcID) { }

			public override void CalculateSubPosition ()
			{
				ThreadManager.AssertIsMainThread();
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
								(type == BuiltinBlocks.Types.air || type == BuiltinBlocks.Types.logtemperate)
								&& (typeAbove == BuiltinBlocks.Types.air || typeAbove == BuiltinBlocks.Types.logtemperate)
								&& typeBelow.IsFertile
							) {
								treeLocation = new Vector3Int(x, min.y + y, z);
								bool canStand;
								Vector3Int position;
								if (AI.PathingManager.TryGetClosestPositionWorldNotAt(treeLocation, NPC.Position, out canStand, out position)) {
									if (canStand) {
										positionSub = position;
									} else {
										positionSub = Vector3Int.invalidPos;
									}
								} else {
									positionSub = Vector3Int.invalidPos;
								}
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

				if (type == BuiltinBlocks.Indices.logtemperate) {
					if (ChopTree(treeLocation, Owner)) {
						state.SetIndicator(new Shared.IndicatorState(10f, BuiltinBlocks.Indices.logtemperate));
						AudioManager.SendAudio(treeLocation.Vector, "woodDeleteHeavy");

						GatherResults.Clear();
						GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.Indices.logtemperate, 3, 1f));
						GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.Indices.leavestemperate, 9, 1f));

						ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, this as IJob, treeLocation, GatherResults);

						NPC.Inventory.Add(GatherResults);
						GatheredItemsCount++;
						if (GatheredItemsCount >= definition.MaxGathersPerRun) {
							shouldDumpInventory = true;
							GatheredItemsCount = 0;
						}
					} else {
						state.SetCooldown(Random.NextFloat(3f, 6f));
					}
					return;
				}

				if (type == 0) {
					// maybe plant sapling?
					if (World.TryGetTypeAt(treeLocation.Add(0, -1, 0), out ItemTypes.ItemType typeBelow)) {
						if (typeBelow.IsFertile) {
							ServerManager.TryChangeBlock(treeLocation, BuiltinBlocks.Types.air, BuiltinBlocks.Types.sappling, Owner, ESetBlockFlags.DefaultAudio);
							state.SetCooldown(2.0);
							return;
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
