using BlockTypes.Builtin;

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

		public void DoJob (IIterationType iterationType, IAreaJob job, ref NPC.NPCBase.NPCState state)
		{
			if (iterationType == null || buildType == null || buildType.ItemIndex == 0) {
				AreaJobTracker.RemoveJob(job);
				return;
			}

			while (true) {
				Vector3Int jobPosition = iterationType.CurrentPosition;

				ushort foundTypeIndex;
				if (World.TryGetTypeAt(jobPosition, out foundTypeIndex)) {
					if (foundTypeIndex == 0) {
						Stockpile ownerStockPile = Stockpile.GetStockPile(job.Owner);
						if (ownerStockPile.Contains(buildType.ItemIndex)) {
							if (ServerManager.TryChangeBlock(jobPosition, buildType.ItemIndex, job.Owner, ServerManager.SetBlockFlags.DefaultAudio)) {
								ownerStockPile.TryRemove(buildType.ItemIndex);
								if (iterationType.MoveNext()) {
									state.SetIndicator(new Shared.IndicatorState(GetCooldown(), buildType.ItemIndex));
								} else {
									// failed to find next position to do job at, self-destruct
									state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
									AreaJobTracker.RemoveJob(job);
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
						if (!iterationType.MoveNext()) {
							// failed to find next position to do job at, self-destruct
							state.SetIndicator(new Shared.IndicatorState(5f, BuiltinBlocks.ErrorIdle));
							AreaJobTracker.RemoveJob(job);
						}
						continue; // found non-air, try next loop
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

		public static float GetCooldown ()
		{
			return Random.NextFloat(3f, 5f);
		}
	}
}
