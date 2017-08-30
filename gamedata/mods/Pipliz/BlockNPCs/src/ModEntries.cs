using Pipliz.APIProvider.Jobs;
using Pipliz.BlockNPCs.Implementations;
using System.Collections.Generic;
using System.IO;

namespace Pipliz.BlockNPCs
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		public static string ModGamedataDirectory;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "pipliz.blocknpcs.assemblyload")]
		public static void OnAssemblyLoaded (string path)
		{
			ModGamedataDirectory = Path.Combine(Path.GetDirectoryName(path), "gamedata/");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterDefiningNPCTypes, "pipliz.blocknpcs.registerjobs")]
		[ModLoader.ModCallbackProvidesFor("pipliz.apiprovider.jobs.resolvetypes")]
		public static void AfterDefiningNPCTypes ()
		{
			BlockJobManagerTracker.Register<FurnaceJob>("furnace");
			BlockJobManagerTracker.Register<GrinderJob>("grindstone");
			BlockJobManagerTracker.Register<MintJob>("mint");
			BlockJobManagerTracker.Register<OvenJob>("oven");
			BlockJobManagerTracker.Register<QuiverJob>("quiver");
			BlockJobManagerTracker.Register<ShopJob>("shop");
			BlockJobManagerTracker.Register<WorkBenchJob>("workbench");
			BlockJobManagerTracker.Register<TailorJob>("tailorshop");
			BlockJobManagerTracker.Register<TechnologistJob>("technologisttable");
			BlockJobManagerTracker.Register<ScientistJob>("sciencelab");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.blocknpcs.registertypes")]
		public static void AfterItemTypesDefined2 ()
		{
			ItemTypesServer.RegisterChangeTypes("furnace", new List<string>()
				{ "furnacex+", "furnacex-", "furnacez+", "furnacez-", "furnacelitx+", "furnacelitx-", "furnacelitz+", "furnacelitz-" }
			);
			ItemTypesServer.RegisterChangeTypes("oven", new List<string>()
				{ "ovenx+", "ovenz+", "ovenx-", "ovenz-", "ovenlitx+", "ovenlitz+", "ovenlitx-", "ovenlitz-" }
			);
		}
	}
}
