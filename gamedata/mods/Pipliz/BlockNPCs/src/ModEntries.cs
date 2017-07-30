using Pipliz.BlockNPCs.Implementations;
using System.Collections.Generic;

namespace Pipliz.BlockNPCs
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterDefiningNPCTypes)]
		public static void AfterDefiningNPCTypes ()
		{
			BlockJobManagerTracker.Register<FurnaceJob>("furnace");
			BlockJobManagerTracker.Register<GrinderJob>("grindstone");
			BlockJobManagerTracker.Register<MintJob>("mint");
			BlockJobManagerTracker.Register<OvenJob>("oven");
			BlockJobManagerTracker.Register<QuiverJob>("quiver");
			BlockJobManagerTracker.Register<ShopJob>("shop");
			BlockJobManagerTracker.Register<WorkBenchJob>("workbench");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined)]
		public static void AfterItemTypesDefined ()
		{
			ItemTypesServer.RegisterChangeTypes("furnace", new List<string>()
				{ "furnacex+", "furnacex-", "furnacez+", "furnacez-", "furnacelitx+", "furnacelitx-", "furnacelitz+", "furnacelitz-" }
			);
			ItemTypesServer.RegisterChangeTypes("oven", new List<string>()
				{ "ovenx+", "ovenz+", "ovenx-", "ovenz-", "ovenlitx+", "ovenlitz+", "ovenlitx-", "ovenlitz-" }
			);
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad)]
		public static void AfterWorldLoad ()
		{
			BlockJobManagerTracker.AfterWorldLoad();
		}

		[ModLoader.ModCallback (ModLoader.EModCallbackType.OnQuit)]
		public static void OnQuit ()
		{
			BlockJobManagerTracker.Save();
		}
	}
}
