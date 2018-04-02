using BlockTypes.Builtin;
using UnityEngine.Assertions;

namespace Pipliz.Mods.BaseGame.Construction.Types
{
	public class BuilderBasic : IConstructionType
	{
		public Shared.EAreaType AreaType { get { return Shared.EAreaType.BuilderArea; } }
		public Shared.EAreaMeshType AreaTypeMesh { get { return Shared.EAreaMeshType.ThreeD; } }

		protected ItemTypes.ItemType buildType;

		public BuilderBasic (ItemTypes.ItemType buildType)
		{
			this.buildType = buildType;
		}

		public void DoJob (IIterationType iterationType, IAreaJob areaJob, ConstructionJob job, ref NPC.NPCBase.NPCState state)
		{
			if (iterationType == null || buildType == null || buildType.ItemIndex == 0) {
				AreaJobTracker.RemoveJob(areaJob);
				return;
			}

			int iMax = 4096;
			while (iMax-- > 0) {
				Vector3Int jobPosition = iterationType.CurrentPosition;

				ushort foundTypeIndex;
				if (World.TryGetTypeAt(jobPosition, out foundTypeIndex)) {
					if (foundTypeIndex == 0 || foundTypeIndex == BuiltinBlocks.Water) {
						Stockpile ownerStockPile = Stockpile.GetStockPile(areaJob.Owner);
						if (ownerStockPile.Contains(buildType.ItemIndex)) {
							if (ServerManager.TryChangeBlock(jobPosition, buildType.ItemIndex, areaJob.Owner, ServerManager.SetBlockFlags.DefaultAudio)) {
								if (--job.storedItems == 0) {
									state.JobIsDone = true;
								}
								ownerStockPile.TryRemove(buildType.ItemIndex);
								if (iterationType.MoveNext()) {
									state.SetIndicator(new Shared.IndicatorState(GetCooldown(), buildType.ItemIndex));
								} else {
									// failed to find next position to do job at, self-destruct
									state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
									AreaJobTracker.RemoveJob(areaJob);
								}
								return;
							} else {
								// shouldn't really happen, world not loaded (just checked)
								state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorMissing, true, false));
							}
						} else {
							// missing building item
							state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(5f, 8f), buildType.ItemIndex, true, false));
						}
						return; // either changed a block or set indicator, job done
					} else {
						// move iterator, not placing at non-air blocks
						if (iterationType.MoveNext()) {
							continue; // found non-air, try next loop
						} else {
							// failed to find next position to do job at, self-destruct
							state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
							AreaJobTracker.RemoveJob(areaJob);
							return;
						}
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

		public static float GetCooldown ()
		{
			return Random.NextFloat(1.5f, 2.5f);
		}
	}
}
