using BlockTypes.Builtin;
using Server.GrowableBlocks;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;
	using JSON;

	[GrowableBlockDefinitionAutoLoader]
	public class CherrySapling : GrowableBlockDefinition<CherrySapling>
	{
		public CherrySapling ()
		{
			FileName = "cherrysapling";
			GrowthType = EGrowthType.Always;
			RandomStartGrowthMax = 9f / 21f; // to maintain random 9-21 hour growth from < 0.5.0
			Stages = new List<IGrowableStage>()
			{
				new GrowableStage("cherrysapling", 21f),
				new GrowableStage()
			};
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

		#region LOAD_LEGACY_BLOCKS_WORKAROUND
		/// <summary>
		/// This #region code is to load the legacy updatableblocks.json data for upgrading from IUpdatableBlocks to Server.Growables
		/// from before v0.5.0 to v0.5.0 and later
		/// </summary>
		JSONNode updatableBlocks;

		public override void StartLoading ()
		{
			// do custom things before base.AsyncLoad so FinishLoading also waits for this to complete
			ThreadPool.QueueUserWorkItem(delegate (object obj)
			{
				try {
					string path = string.Format("gamedata/savegames/{0}/updatableblocks.json", ServerManager.WorldName);
					if (File.Exists(path)) {
						JSON.Deserialize(path, out updatableBlocks, false);
					}
				} finally {
					AsyncLoad(obj);
				}
			});
		}

		public override void FinishLoading ()
		{
			base.FinishLoading();
			if (updatableBlocks != null) {
				JSONNode array;
				if (updatableBlocks.TryGetChild("cherrysapling", out array)) {
					Log.Write("Loading {0} legacy blocks to type cherrysapling", array.ChildCount);
					for (int i = 0; i < array.ChildCount; i++) {
						GrowableBlockManager.RegisterGrowableBlock(MakeGrowableBlockLegacy(array[i]));
					}
				}
			}
		}

		#endregion LOAD_LEGACY_BLOCKS_WORKAROUND

		public override bool TryAdvanceStage (IGrowableBlock block, byte currentStageIndex)
		{
			if (currentStageIndex == 0) {
				Vector3Int pos = block.Position;
				for (int i = 0; i < logs.Count; i++) {
					ushort currentType;
					if (World.TryGetTypeAt(pos + logs[i], out currentType)) {
						if (currentType == 0 || currentType == BuiltinBlocks.CherrySapling) {
							if (!ServerManager.TryChangeBlock(pos + logs[i], BuiltinBlocks.LogTemperate)) {
								return false; // not loaded
							}
						}
					} else {
						return false; // not loaded
					}
				}
				for (int i = 0; i < leaves.Count; i++) {
					ushort currentType;
					if (World.TryGetTypeAt(pos + leaves[i], out currentType)) {
						if (currentType == 0) {
							if (!ServerManager.TryChangeBlock(pos + leaves[i], BuiltinBlocks.CherryBlossom)) {
								return false; // not loaded
							}
						}
					} else {
						return false; // not loaded
					}
				}
			}
			// succesfully grew, or invalid stage index. Either case, done.
			block.SetInvalid();
			return true;
		}
	}
}
