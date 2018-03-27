using BlockTypes.Builtin;
using UnityEngine.Assertions;

namespace Pipliz.Mods.BaseGame.Construction.Types
{
	public class DiggerBasic : IConstructionType
	{
		public Shared.EAreaType AreaType { get { return Shared.EAreaType.DiggerArea; } }
		public Shared.EAreaMeshType AreaTypeMesh { get { return Shared.EAreaMeshType.ThreeD; } }

		static System.Collections.Generic.List<ItemTypes.ItemTypeDrops> GatherResults = new System.Collections.Generic.List<ItemTypes.ItemTypeDrops>();

		public void DoJob (IIterationType iterationType, IAreaJob areaJob, ConstructionJob job, ref NPC.NPCBase.NPCState state)
		{
			if (iterationType == null) {
				AreaJobTracker.RemoveJob(areaJob);
				return;
			}

			int iMax = 4096;
			while (iMax-- > 0) {
				Vector3Int jobPosition = iterationType.CurrentPosition;
				if (!jobPosition.IsValid) {
					// failed to find next position to do job at, self-destruct
					state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
					AreaJobTracker.RemoveJob(areaJob);
					return;
				}
				ushort foundTypeIndex;
				if (World.TryGetTypeAt(jobPosition, out foundTypeIndex)) {
					iterationType.MoveNext();
					if (foundTypeIndex != 0) {
						ItemTypes.ItemType foundType = ItemTypes.GetType(foundTypeIndex);

						if (!foundType.IsDestructible) {
							continue; // skip this block, retry
						}

						if (ServerManager.TryChangeBlock(jobPosition, 0, areaJob.Owner, ServerManager.SetBlockFlags.DefaultAudio)) {
							float blockDestructionTime = GetCooldown(foundType.DestructionTime * 0.001f);
							GatherResults.Clear();
							var itemList = foundType.OnRemoveItems;
							for (int i = 0; i < itemList.Count; i++) {
								GatherResults.Add(itemList[i]);
							}

							ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, job as NPC.IJob, jobPosition, GatherResults);

							InventoryItem toShow = ItemTypes.ItemTypeDrops.GetWeightedRandom(GatherResults);
							if (toShow.Amount > 0) {
								state.SetIndicator(new Shared.IndicatorState(blockDestructionTime, toShow.Type));
							} else {
								state.SetCooldown(blockDestructionTime);
							}
							state.Inventory.Add(GatherResults);
						} else {
							state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorMissing, true, false));
						}
						return; // either changed a block or set indicator, job done
					} else {
						continue; // found air, try next loop
					}
					// unreachable
				} else {
					state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorMissing, true, false));
					return; // end loop, wait for world to load
				}
				// unreachable
			}
			// reached loop count limit
			Assert.IsTrue(iMax <= 0);
			state.SetCooldown(1.0);
		}

		public static float GetCooldown (float blockDestructionTime)
		{
			return Math.Clamp(Random.NextFloat(4f * blockDestructionTime, 6f * blockDestructionTime), 0.2f, 15f);
		}
	}
}
