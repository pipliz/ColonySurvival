using BlockTypes;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	public class CherrySapling : BaseGrowableBlockDefinition
	{
		public CherrySapling ()
		{
			GrowthType = EGrowthType.Always;
			RandomStartGrowthMax = 9f / 21f; // random 12-21 hour growth

			SetStages(new List<GrowableStage>()
			{
				new GrowableStage("cherrysapling", 21f),
				new GrowableStage() // filler stage, so the logic thinks it's advancing to something
			});
		}

		public override void TryAdvanceStage (GrowableBlock block, Chunk chunk, Vector3Int blockPosition)
		{
			if (block.StageIndex == 0) {
				// set all logs
				for (int i = 0; i < logs.Count; i++) {
					ushort currentType;
					if (World.TryGetTypeAt(blockPosition + logs[i], out currentType)) {
						if (logs[i] == Vector3Int.zero) {
							// don't trigger the sappling entity removal callback - it'll deadlock (removing itself)
							if (!ServerManager.TryChangeBlock(blockPosition + logs[i], BuiltinBlocks.LogTemperate, flags: ServerManager.SetBlockFlags.Default & ~ServerManager.SetBlockFlags.TriggerEntityCallbacks)) {
								return; // not loaded
							}
						} else {
							if (currentType == 0) {
								if (!ServerManager.TryChangeBlock(blockPosition + logs[i], BuiltinBlocks.LogTemperate)) {
									return; // not loaded
								}
							}
						}
					} else {
						return; // not loaded
					}
				}
				// set all leaves
				for (int i = 0; i < leaves.Count; i++) {
					ushort currentType;
					if (World.TryGetTypeAt(blockPosition + leaves[i], out currentType)) {
						if (leaves[i] == Vector3Int.zero) {
							// don't trigger the sappling entity removal callback - it'll deadlock (removing itself)
							if (!ServerManager.TryChangeBlock(blockPosition + leaves[i], BuiltinBlocks.CherryBlossom, flags: ServerManager.SetBlockFlags.Default & ~ServerManager.SetBlockFlags.TriggerEntityCallbacks)) {
								return; // not loaded
							}
						} else {
							if (currentType == 0) {
								if (!ServerManager.TryChangeBlock(blockPosition + leaves[i], BuiltinBlocks.CherryBlossom)) {
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
			new Vector3Int(0, 0, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(0, 2, 0),
			new Vector3Int(0, 3, 0),
			new Vector3Int(0, 4, 0),
			new Vector3Int(0, 5, 0)
		};

		static List<Vector3Int> leaves = new List<Vector3Int>()
		{
			new Vector3Int(-3, 4, -1),
			new Vector3Int(-3, 4, 0),
			new Vector3Int(-3, 4, 1),
			new Vector3Int(-2, 3, 0),
			new Vector3Int(-2, 4, -2),
			new Vector3Int(-2, 4, -1),
			new Vector3Int(-2, 4, 0),
			new Vector3Int(-2, 4, 1),
			new Vector3Int(-2, 4, 2),
			new Vector3Int(-2, 5, -1),
			new Vector3Int(-2, 5, 0),
			new Vector3Int(-2, 5, 1),
			new Vector3Int(-1, 3, -1),
			new Vector3Int(-1, 3, 0),
			new Vector3Int(-1, 3, 1),
			new Vector3Int(-1, 4, -3),
			new Vector3Int(-1, 4, -2),
			new Vector3Int(-1, 4, -1),
			new Vector3Int(-1, 4, 0),
			new Vector3Int(-1, 4, 1),
			new Vector3Int(-1, 4, 2),
			new Vector3Int(-1, 4, 3),
			new Vector3Int(-1, 5, -2),
			new Vector3Int(-1, 5, -1),
			new Vector3Int(-1, 5, 0),
			new Vector3Int(-1, 5, 1),
			new Vector3Int(-1, 5, 2),
			new Vector3Int(-1, 6, 0),
			new Vector3Int(0, 3, -2),
			new Vector3Int(0, 3, -1),
			new Vector3Int(0, 3, 1),
			new Vector3Int(0, 3, 2),
			new Vector3Int(0, 4, -3),
			new Vector3Int(0, 4, -2),
			new Vector3Int(0, 4, -1),
			new Vector3Int(0, 4, 1),
			new Vector3Int(0, 4, 2),
			new Vector3Int(0, 4, 3),
			new Vector3Int(0, 5, -2),
			new Vector3Int(0, 5, -1),
			new Vector3Int(0, 5, 1),
			new Vector3Int(0, 5, 2),
			new Vector3Int(0, 6, -1),
			new Vector3Int(0, 6, 0),
			new Vector3Int(0, 6, 1),
			new Vector3Int(3, 4, -1),
			new Vector3Int(3, 4, 0),
			new Vector3Int(3, 4, 1),
			new Vector3Int(2, 3, 0),
			new Vector3Int(2, 4, -2),
			new Vector3Int(2, 4, -1),
			new Vector3Int(2, 4, 0),
			new Vector3Int(2, 4, 1),
			new Vector3Int(2, 4, 2),
			new Vector3Int(2, 5, -1),
			new Vector3Int(2, 5, 0),
			new Vector3Int(2, 5, 1),
			new Vector3Int(1, 3, -1),
			new Vector3Int(1, 3, 0),
			new Vector3Int(1, 3, 1),
			new Vector3Int(1, 4, -3),
			new Vector3Int(1, 4, -2),
			new Vector3Int(1, 4, -1),
			new Vector3Int(1, 4, 0),
			new Vector3Int(1, 4, 1),
			new Vector3Int(1, 4, 2),
			new Vector3Int(1, 4, 3),
			new Vector3Int(1, 5, -2),
			new Vector3Int(1, 5, -1),
			new Vector3Int(1, 5, 0),
			new Vector3Int(1, 5, 1),
			new Vector3Int(1, 5, 2),
			new Vector3Int(1, 5, 0)
		};
	}
}
