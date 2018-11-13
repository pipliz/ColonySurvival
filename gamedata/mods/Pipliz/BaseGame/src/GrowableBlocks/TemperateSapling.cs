using BlockEntities;
using GrowableBlocks;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	[BlockEntityAutoLoader]
	public class TemperateSapling : BaseGrowableBlockDefinition
	{
		public static List<Vector3Int> Logs { get; } = new List<Vector3Int>()
		{
			new Vector3Int(0,0,0),
			new Vector3Int(0,1,0),
			new Vector3Int(0,2,0),
			new Vector3Int(0,3,0)
		};

		public static List<Vector3Int> Leaves { get; } = new List<Vector3Int>()
		{
			new Vector3Int(01,3,01),
			new Vector3Int(01,3,00),
			new Vector3Int(01,3,-1),
			new Vector3Int(00,3,-1),
			new Vector3Int(-1,3,-1),
			new Vector3Int(-1,3,00),
			new Vector3Int(-1,3,01),
			new Vector3Int(00,3,01),
			new Vector3Int(00,4,00)
		};

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
			// don't trigger the sappling removal callbacks - nested change callbacks are not supported (deadlocks)
			// assume only dumb blocks are placed, and only air (& this growable) are replaced
			ESetBlockFlags flags = ESetBlockFlags.Default & ~(ESetBlockFlags.TriggerEntityCallbacks | ESetBlockFlags.TriggerNeighbourCallbacks);
			if (block.StageIndex == 0) {
				ItemTypes.ItemType logType = ItemTypes.GetType("logtemperate");
				ItemTypes.ItemType leavesType = ItemTypes.GetType("leavestemperate");

				// set all logs
				for (int i = 0; i < Logs.Count; i++) {
					ItemTypes.ItemType oldType = Logs[i] == Vector3Int.zero ? null : ItemTypes.Air;
					if (ServerManager.TryChangeBlock(blockPosition + Logs[i], oldType, logType, flags: flags) == EServerChangeBlockResult.ChunkNotReady) {
						return;
					}
				}

				// set all leaves
				for (int i = 0; i < Leaves.Count; i++) {
					ItemTypes.ItemType oldType = Leaves[i] == Vector3Int.zero ? null : ItemTypes.Air;
					if (ServerManager.TryChangeBlock(blockPosition + Leaves[i], oldType, leavesType, flags: flags) == EServerChangeBlockResult.ChunkNotReady) {
						return;
					}
				}
			}
			// succesfully grew, or invalid stage index. Either case, done.
			block.SetInvalid();
		}
	}
}
