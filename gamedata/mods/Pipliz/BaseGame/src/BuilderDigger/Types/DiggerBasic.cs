using BlockTypes.Builtin;

namespace Pipliz.Mods.BaseGame.Construction.Types
{
	public class DiggerBasic : IConstructionType
	{
		public Shared.EAreaType AreaType { get { return Shared.EAreaType.DiggerArea; } }
		public Shared.EAreaMeshType AreaTypeMesh { get { return Shared.EAreaMeshType.ThreeD; } }

		public void DoJob (IIterationType iterationType, IAreaJob job, ref NPC.NPCBase.NPCState state)
		{
			if (iterationType == null) {
				AreaJobTracker.RemoveJob(job);
				return;
			}

			while (true) {
				Vector3Int jobPosition = iterationType.CurrentPosition;

				ushort foundTypeIndex;
				if (World.TryGetTypeAt(jobPosition, out foundTypeIndex)) {
					if (!iterationType.MoveNext()) {
						// failed to find next position to do job at, self-destruct
						state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
						AreaJobTracker.RemoveJob(job);
						return;
					}
					if (foundTypeIndex != 0) {
						ItemTypes.ItemType foundType = ItemTypes.GetType(foundTypeIndex);

						if (!foundType.IsDestructible) {
							continue; // skip this block, retry
						}

						if (ServerManager.TryChangeBlock(jobPosition, 0, job.Owner, ServerManager.SetBlockFlags.DefaultAudio)) {
							InventoryItem typeDropped = InventoryItem.Empty;
							var onRemoveItems = ItemTypes.GetType(foundTypeIndex).OnRemoveItems;
							for (int i = 0; i < onRemoveItems.Count; i++) {
								if (Random.NextDouble() <= onRemoveItems[i].chance) {
									typeDropped = onRemoveItems[i].item;
									state.Inventory.Add(typeDropped);
								}
							}

							float blockDestructionTime = GetCooldown(foundType.DestructionTime * 0.001f);
							if (typeDropped.Amount > 0) {
								state.SetIndicator(new Shared.IndicatorState(blockDestructionTime, typeDropped.Type));
							} else {
								state.SetCooldown(blockDestructionTime);
							}
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
			// unreachable
		}

		public static float GetCooldown (float blockDestructionTime)
		{
			return Math.Clamp(Random.NextFloat(4f * blockDestructionTime, 6f * blockDestructionTime), 0.2f, 15f);
		}
	}
}
