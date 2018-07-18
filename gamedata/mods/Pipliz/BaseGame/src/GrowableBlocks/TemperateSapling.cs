using BlockTypes.Builtin;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	public class TemperateSapling : BaseGrowableBlockDefinition
	{
		public TemperateSapling ()
		{
			GrowthType = EGrowthType.Always;
			RandomStartGrowthMax = 9f / 21f; // random 12-21 hour growth

			SetStages(new List<GrowableStage>()
			{
				new GrowableStage("sappling", 21f),
				new GrowableStage() // filler stage, so the logic thinks it's advancing to something
			});
		}

		public override void TryAdvanceStage (GrowableBlock block, Chunk chunk, Vector3Int blockPosition)
		{
			if (block.StageIndex == 0) {
				for (int i = 0; i < logs.Count; i++) {
					ushort currentType;
					if (World.TryGetTypeAt(blockPosition + logs[i], out currentType)) {
						if (logs[i] == Vector3Int.zero) {
							// don't trigger the sappling entity removal callback - it'll deadlock (removing itself)
							if (!ServerManager.TryChangeBlock(blockPosition + logs[i], BuiltinBlocks.LogTemperate, flags: ServerManager.SetBlockFlags.Default & ~ServerManager.SetBlockFlags.TriggerEntityCallbacks)) {
								return; // not loaded
							}
						} else {
							if (currentType == 0 || currentType == BuiltinBlocks.Sapling) {
								if (!ServerManager.TryChangeBlock(blockPosition + logs[i], BuiltinBlocks.LogTemperate)) {
									return; // not loaded
								}
							}
						}
					} else {
						return; // not loaded
					}
				}
				for (int i = 0; i < leaves.Count; i++) {
					ushort currentType;
					if (World.TryGetTypeAt(blockPosition + leaves[i], out currentType)) {
						if (leaves[i] == Vector3Int.zero) {
							// don't trigger the sappling entity removal callback - it'll deadlock (removing itself)
							if (!ServerManager.TryChangeBlock(blockPosition + leaves[i], BuiltinBlocks.LeavesTemperate, flags: ServerManager.SetBlockFlags.Default & ~ServerManager.SetBlockFlags.TriggerEntityCallbacks)) {
								return; // not loaded
							}
						} else {
							if (currentType == 0) {
								if (!ServerManager.TryChangeBlock(blockPosition + leaves[i], BuiltinBlocks.LeavesTemperate)) {
									return; // not loaded
								}
							}
						}
					} else {
						return; // not loaded
					}
				}
			}
			// succesfully grew, or invalid stage index. Either case, done.
			block.SetInvalid();
		}

		static List<Vector3Int> logs = new List<Vector3Int>()
		{
			new Vector3Int(0,0,0),
			new Vector3Int(0,1,0),
			new Vector3Int(0,2,0)
		};

		static List<Vector3Int> leaves = new List<Vector3Int>()
		{
			new Vector3Int(1,2,1),
			new Vector3Int(1,2,0),
			new Vector3Int(1,2,-1),
			new Vector3Int(0,2,-1),
			new Vector3Int(-1,2,-1),
			new Vector3Int(-1,2,0),
			new Vector3Int(-1,2,1),
			new Vector3Int(0,2,1),
			new Vector3Int(0,3,0)
		};
	}
}
