using Server.GrowableBlocks;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;
	using JSON;

	[GrowableBlockDefinitionAutoLoader]
	public class Flax : GrowableBlockDefinition<Flax>
	{
		public Flax ()
		{
			FileName = "flax";
			GrowthType = EGrowthType.FirstNightRandom;
			Stages = new List<IGrowableStage>()
			{
				new GrowableStage("flaxstage1", TimeCycle.NightLength - 0.5f),
				new GrowableStage("flaxstage2")
			};
		}

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
				} catch (System.Exception e) {
					Log.WriteException(e);
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
				if (updatableBlocks.TryGetChild("flax", out array)) {
					Log.Write("Loading {0} legacy blocks to type flax", array.ChildCount);
					for (int i = 0; i < array.ChildCount; i++) {
						GrowableBlockManager.RegisterGrowableBlock(MakeGrowableBlockLegacy(array[i]));
					}
				}
			}
		}

		#endregion LOAD_LEGACY_BLOCKS_WORKAROUND
	}
}
