using BlockTypes;
using Jobs;
using NPC;
using System.Collections.Generic;
using System.Linq;
using TerrainGeneration;

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

		public class ForesterJob : AbstractAreaJob
		{
			// store treeLocation separately from positionSub because the farmer will move next to these positions(they're not equal)
			protected Vector3Int treeLocation = Vector3Int.invalidPos;
			protected ItemTypes.ItemType[] saplingTypes;

			static List<ItemTypes.ItemTypeDrops> GatherResults = new List<ItemTypes.ItemTypeDrops>();
			static ItemTypes.ItemType[] yTypesBuffer = new ItemTypes.ItemType[5]; // max 3 Y + 1 below + 1 above
			static List<ushort> countableTypes;
			static Dictionary<ushort, int> spawnedTypesBuffer = new Dictionary<ushort, int>();

			static readonly Vector3Int[] LeaveOffsets = new Vector3Int[]
			{
				new Vector3Int(1, 3, 1),
				new Vector3Int(1, 3, 0),
				new Vector3Int(1, 3, -1),
				new Vector3Int(0, 3, -1),
				new Vector3Int(-1, 3, -1),
				new Vector3Int(-1, 3, 0),
				new Vector3Int(-1, 3, 1),
				new Vector3Int(0, 3, 1),
				new Vector3Int(0, 4, 0)
			};

			static readonly Vector3Int[] LogOffsets = new Vector3Int[]
			{
				new Vector3Int(0, 0, 0),
				new Vector3Int(0, 1, 0),
				new Vector3Int(0, 2, 0),
				new Vector3Int(0, 3, 0)
			};

			public ForesterJob (AbstractAreaJobDefinition def, Colony owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(def, owner, min, max, npcID) { }

			public override void CalculateSubPosition ()
			{
				ThreadManager.AssertIsMainThread();

				Vector3Int min = Minimum;
				Vector3Int max = Maximum;
				int ySize = max.y - min.y + 1;

				if (saplingTypes == null) {
					FindTreeType((min + (max - min) / 2), spawnedTypesBuffer);
					saplingTypes = GetSaplingTypes(spawnedTypesBuffer);
				}

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
								(type == BuiltinBlocks.Types.air || type.HasBehaviour("log"))
								&& (typeAbove == BuiltinBlocks.Types.air || typeAbove.HasBehaviour("log"))
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

			static ItemTypes.ItemType[] GetSaplingTypes (Dictionary<ushort, int> leavesLogsTypes)
			{
				List<ItemTypes.ItemType> tempList = new List<ItemTypes.ItemType>();

				if (leavesLogsTypes.ContainsKey(BuiltinBlocks.Indices.logtemperate)) {
					tempList.Add(BuiltinBlocks.Types.sappling);
				}
				if (leavesLogsTypes.ContainsKey(BuiltinBlocks.Indices.logtaiga)) {
					tempList.Add(BuiltinBlocks.Types.saplingtaiga);
				}
				if (leavesLogsTypes.ContainsKey(BuiltinBlocks.Indices.leavesfallorange)
					|| leavesLogsTypes.ContainsKey(BuiltinBlocks.Indices.leavesfallred)
					|| leavesLogsTypes.ContainsKey(BuiltinBlocks.Indices.leavesfallyellow)
				) {
					tempList.Add(BuiltinBlocks.Types.saplingfallorange);
				}

				if (tempList.Count == 0) {
					tempList.Add(BuiltinBlocks.Types.saplingtaiga);
				}

				if (leavesLogsTypes.ContainsKey(BuiltinBlocks.Indices.cherryblossom)) {
					tempList.Add(BuiltinBlocks.Types.cherrysaplingsmall);
				}

				return tempList.ToArray();
			}

			static void FindTreeType (Vector3Int minMid, Dictionary<ushort, int> spawnedTypesBuffer)
			{
				ThreadManager.AssertIsMainThread();
				if (countableTypes == null) {
					countableTypes = ItemTypes._TypeByUShort.Values
						.Where(val => val.HasBehaviour("log"))
						.Concat(ItemTypes._TypeByUShort.Values.Where(val => val.HasBehaviour("leaves")))
						.Select(type => type.ItemIndex)
						.ToList();
				}

				spawnedTypesBuffer.Clear();
				spawnedTypesBuffer[BuiltinBlocks.Indices.logtemperate] = 3;
				spawnedTypesBuffer[BuiltinBlocks.Indices.leavestemperate] = 10;

				if (!(ServerManager.TerrainGenerator is TerrainGenerator terrainGen)) {
					return;
				}
				var gen = terrainGen.StructureGenerator;
				while (gen != null && !(gen is TerrainGenerator.DefaultTreeStructureGenerator)) {
					gen = gen.InnerGenerator;
				}
				if (gen == null) {
					return;
				}
				var treeGen = (TerrainGenerator.DefaultTreeStructureGenerator)gen;
				terrainGen.QueryTempPrecip(minMid.x, minMid.z, out float temp, out float precip);
				TerrainGenerator.MetaBiomeLocation metaBiomeLocation = terrainGen.MetaBiomeProvider.GetChunkMetaBiomeLocation(minMid.x, minMid.z);

				int max = treeGen.ItemIndices.Length - 1;
				for (int itemIndex = treeGen.ItemIndices[Math.Clamp((int)(temp - TerrainGenerator.MinimumTemperature), 0, max)]; itemIndex < treeGen.ItemsLocations.Length; itemIndex++) {
					TerrainGenerator.BiomeConfigLocation location = treeGen.ItemsLocations[itemIndex];
					if (!location.IsValid(temp, precip)) {
						continue;
					}
					if (temp + 1f < location.MinTemperature) {
						return;
					}
					if (!treeGen.RequiresMetaBiome[itemIndex].Contains(metaBiomeLocation)) {
						continue;
					}

					TerrainGenerator.DefaultTreeStructureGenerator.ItemPointerData items = treeGen.ItemsGrid[itemIndex];
					if (items.Pointers == null) {
						return;
					}

					for (int pointerIndex = 0; pointerIndex < items.Pointers.Length; pointerIndex++) {
						TerrainGenerator.DefaultTreeStructureGenerator.ItemPointer item = items.Pointers[pointerIndex];
						StructureBlock[] blocks = treeGen.ItemBlocks[item.Index].Blocks;
						if (blocks == null || blocks.Length == 0) {
							continue;
						}
						for (int blockIndex = 0; blockIndex < blocks.Length; blockIndex++) {
							StructureBlock block = blocks[blockIndex];
							if (countableTypes.Contains(block.Type)) {
								if (spawnedTypesBuffer.TryGetValue(block.Type, out int val)) {
									spawnedTypesBuffer[block.Type] = val + 1;
								} else {
									spawnedTypesBuffer[block.Type] = 1;
								}
							}
						}
					}
				}
			}

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

				ItemTypes.ItemType type;
				if (!World.TryGetTypeAt(treeLocation, out type)) {
					state.SetCooldown(10.0);
					return;
				}

				if (type.HasBehaviour("log")) {
					GatherResults.Clear();
					ItemTypes.ItemType lastRemovedType;
					if (ChopTree()) {
						if (lastRemovedType != null) {
							state.SetIndicator(new Shared.IndicatorState(10f, BuiltinBlocks.Indices.logtemperate));
						} else {
							state.SetCooldown(10f);
						}

						AudioManager.SendAudio(treeLocation.Vector, "woodDeleteHeavy");

						ModLoader.Callbacks.OnNPCGathered.Invoke(this, treeLocation, GatherResults);

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

					bool ChopTree ()
					{
						lastRemovedType = default;
						for (int i = 0; i < LeaveOffsets.Length; i++) {
							if (World.TryGetTypeAt(treeLocation + LeaveOffsets[i], out ItemTypes.ItemType foundType) && foundType.HasBehaviour("leaves")) {
								switch (ServerManager.TryChangeBlock(treeLocation + LeaveOffsets[i], foundType, BuiltinBlocks.Types.air, Owner)) {
									case EServerChangeBlockResult.Success:
										lastRemovedType = foundType;
										GatherResults.Add(lastRemovedType.OnRemoveItems);
										break;
									case EServerChangeBlockResult.CancelledByCallback:
									case EServerChangeBlockResult.ChunkNotReady:
										return false;
									default:
										break;
								}
							}
						}
						for (int i = 0; i < LogOffsets.Length; i++) {
							if (World.TryGetTypeAt(treeLocation + LogOffsets[i], out ItemTypes.ItemType foundType) && foundType.HasBehaviour("log")) {
								switch (ServerManager.TryChangeBlock(treeLocation + LogOffsets[i], foundType, BuiltinBlocks.Types.air, Owner)) {
									case EServerChangeBlockResult.Success:
										lastRemovedType = foundType;
										GatherResults.Add(lastRemovedType.OnRemoveItems);
										break;
									case EServerChangeBlockResult.CancelledByCallback:
									case EServerChangeBlockResult.ChunkNotReady:
										return false;
									default:
										break;
								}
							}
						}
						return true;
					}
				} else if (type == BuiltinBlocks.Types.air) {
					// maybe plant sapling?
					if (World.TryGetTypeAt(treeLocation.Add(0, -1, 0), out ItemTypes.ItemType typeBelow)) {
						if (typeBelow.IsFertile && saplingTypes.Length > 0) {
							int saplingIdx = Random.Next(0, saplingTypes.Length);
							ServerManager.TryChangeBlock(treeLocation, BuiltinBlocks.Types.air, saplingTypes[saplingIdx], Owner, ESetBlockFlags.DefaultAudio);
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
