using Pipliz.Mods.APIProvider.Jobs;
using System.IO;

namespace Pipliz.Mods.BaseGame.BlockNPCs
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

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.blocknpcs.registerjobs")]
		[ModLoader.ModCallbackProvidesFor("pipliz.apiprovider.jobs.resolvetypes")]
		public static void AfterDefiningNPCTypes ()
		{
			BlockJobManagerTracker.Register<Construction.ConstructionJob>("constructionjob");
			BlockJobManagerTracker.Register<MinerJob>("minerjob");
			BlockJobManagerTracker.Register<WoodcutterJob>("splittingstump");
			BlockJobManagerTracker.Register<BloomeryJob>("bloomery");
			BlockJobManagerTracker.Register<FineryForgeJob>("fineryforge");
			BlockJobManagerTracker.Register<FurnaceJob>("furnace");
			BlockJobManagerTracker.Register<GrinderJob>("grindstone");
			BlockJobManagerTracker.Register<GunSmithJob>("gunsmithshop");
			BlockJobManagerTracker.Register<KilnJob>("kiln");
			BlockJobManagerTracker.Register<MetalSmithJob>("bronzeanvil");
			BlockJobManagerTracker.Register<MintJob>("mint");
			BlockJobManagerTracker.Register<OvenJob>("oven");
			BlockJobManagerTracker.Register<GuardBowJobDay>("guardbowdayjob");
			BlockJobManagerTracker.Register<GuardBowJobNight>("guardbownightjob");
			BlockJobManagerTracker.Register<GuardCrossbowJobDay>("guardcrossbowdayjob");
			BlockJobManagerTracker.Register<GuardCrossbowJobNight>("guardcrossbownightjob");
			BlockJobManagerTracker.Register<GuardMatchlockJobDay>("guardmatchlockdayjob");
			BlockJobManagerTracker.Register<GuardMatchlockJobNight>("guardmatchlocknightjob");
			BlockJobManagerTracker.Register<GuardSlingerJobDay>("guardslingerdayjob");
			BlockJobManagerTracker.Register<GuardSlingerJobNight>("guardslingernightjob");
			BlockJobManagerTracker.Register<ScientistJob>("sciencelab");
			BlockJobManagerTracker.Register<ShopJob>("shop");
			BlockJobManagerTracker.Register<TailorJob>("tailorshop");
			BlockJobManagerTracker.Register<TechnologistJob>("technologisttable");
			BlockJobManagerTracker.Register<WorkBenchJob>("workbench");
			BlockJobManagerTracker.Register<StonemasonJob>("stonemasonworkbench");
			BlockJobManagerTracker.Register<DyerJob>("dyertable");
		}
	}
}
