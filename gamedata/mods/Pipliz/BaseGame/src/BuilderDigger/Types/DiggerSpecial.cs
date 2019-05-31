using BlockTypes;
using Jobs;
using UnityEngine.Assertions;

namespace Pipliz.Mods.BaseGame.Construction.Types
{
	public class DiggerSpecial : IConstructionType
	{
		public int MaxGatheredPerRun { get; set; } = 5;
		public int OnStockpileNewItemCount => 0;

		static System.Collections.Generic.List<ItemTypes.ItemTypeDrops> GatherResults = new System.Collections.Generic.List<ItemTypes.ItemTypeDrops>();

		protected ItemTypes.ItemType digType;

		public DiggerSpecial (ItemTypes.ItemType digType)
		{
			this.digType = digType;
		}

		public void DoJob (IIterationType iterationType, IAreaJob areaJob, ConstructionJobInstance job, ref NPC.NPCBase.NPCState state)
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
					state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.Indices.erroridle));
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

						if (foundType != digType) {
							bool success = false;
							var parentType = foundType.ParentItemType;
							while (parentType != null) {
								if (parentType == digType) {
									success = true;
									break;
								} else {
									parentType = parentType.ParentItemType;
								}
							}

							if (!success) {
								continue;
							}
						}

						if (ServerManager.TryChangeBlock(jobPosition, 0, areaJob.Owner, ESetBlockFlags.DefaultAudio) == EServerChangeBlockResult.Success) {
							float blockDestructionTime = GetCooldown(foundType.DestructionTime * 0.001f);
							GatherResults.Clear();
							var itemList = foundType.OnRemoveItems;
							for (int i = 0; i < itemList.Count; i++) {
								GatherResults.Add(itemList[i]);
							}

							ModLoader.Callbacks.OnNPCGathered.Invoke(job, jobPosition, GatherResults);

							InventoryItem toShow = ItemTypes.ItemTypeDrops.GetWeightedRandom(GatherResults);
							if (toShow.Amount > 0) {
								state.SetIndicator(new Shared.IndicatorState(blockDestructionTime, toShow.Type));
							} else {
								state.SetCooldown(blockDestructionTime);
							}
							state.Inventory.Add(GatherResults);

							job.StoredItemCount++;
							if (job.StoredItemCount >= MaxGatheredPerRun) {
								job.ShouldTakeItems = true;
								state.JobIsDone = true;
							}
						} else {
							state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.Indices.missingerror, true, false));
						}
						return; // either changed a block or set indicator, job done
					} else {
						continue; // found air, try next loop
					}
					// unreachable
				} else {
					state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.Indices.missingerror, true, false));
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
			return Math.Clamp(Random.NextFloat(4f * blockDestructionTime, 6f * blockDestructionTime) * ServerManager.ServerSettings.NPCs.DiggerCooldownMultiplierSeconds, 0.05f, 15f);
		}
	}
}
