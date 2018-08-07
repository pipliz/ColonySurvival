using BlockTypes;
using Pipliz.APIProvider.Jobs;
using Pipliz.Mods.BaseGame.Construction;
using System.IO;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		public static string ModGamedataDirectory;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "pipliz.blocknpcs.assemblyload")]
		[ModLoader.ModDocumentation("Sets BaseGame gamedata directory")]
		public static void OnAssemblyLoaded (string path)
		{
			ModGamedataDirectory = Path.Combine(Path.GetDirectoryName(path), "gamedata/");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.blocknpcs.registerjobs")]
		[ModLoader.ModDocumentation("Adds all the job block implementations to BlockJobManagerTracker")]
		[ModLoader.ModCallbackDependsOn("create_servermanager_trackers")]
		[ModLoader.ModCallbackProvidesFor("create_savemanager")]
		public static void AfterDefiningNPCTypes ()
		{
			/// rough description of how the blockjobs work here:
			/// - BlockJobManager, instantiated once and passed to the blockentities code. It takes care of receiving loading/block event callbacks.
			/// - BlockJobInstance, instantiated for every ingame job instance by the BlockJobManager
			/// - BlockJobSettings, BlockJobInstances' brain, instantiated once (passed to the manager, which passes it to the instances).
			///						Used so that one instance type can cover a lot of jobs

			CreateJob(new CraftingJobSettings("dyertable", "pipliz.dyer", 5f, 4));
			CreateJob(new CraftingJobSettings("grindstone", "pipliz.grinder", 2.9f, 6));
			CreateJob(new CraftingJobSettings("gunsmithshop", "pipliz.gunsmithjob", 6f, 3));
			CreateJob(new CraftingJobSettings("bronzeanvil", "pipliz.metalsmithjob", 5f, 3, "anvil"));
			CreateJob(new CraftingJobSettings("mint", "pipliz.minter", 10f, 2));
			CreateJob(new CraftingJobSettings("shop", "pipliz.merchant", 10f, 1));
			CreateJob(new CraftingJobSettings("stonemasonworkbench", "pipliz.stonemason", 5f, 3));
			CreateJob(new CraftingJobSettings("tailorshop", "pipliz.tailor", 6f, 2));
			CreateJob(new CraftingJobSettings("technologisttable", "pipliz.technologist", 10f, 2));
			CreateJob(new CraftingJobSettings("splittingstump", "pipliz.woodcutter", 4f, 5, "woodCut"));
			CreateJob(new CraftingJobSettings("workbench", "pipliz.crafter", 8f, 5, "crafting"));
			CreateJob(new CraftingJobRotatedSettings("kiln", "pipliz.kilnjob", 6f, 3));
			CreateJob(new CraftingJobRotatedLitSettings("bloomery", "pipliz.bloomeryjob", 6f, 3));
			CreateJob(new CraftingJobRotatedLitSettings("fineryforge", "pipliz.fineryforgejob", 6f, 3));
			CreateJob(new CraftingJobRotatedLitSettings("furnace", "pipliz.smelter", 7.5f, 2));
			CreateJob(new CraftingJobRotatedLitSettings("oven", "pipliz.baker", 8.3f, 3));

			CreateJob(new GuardJobSettings("guardslingerdayjob", "pipliz.guardslingerday", GuardJobSettings.EGuardSleepType.Night, 50f, 12, 3f, "sling", new InventoryItem(BuiltinBlocks.SlingBullet), new InventoryItem(BuiltinBlocks.Sling)));
			CreateJob(new GuardJobSettings("guardslingernightjob", "pipliz.guardslingernight", GuardJobSettings.EGuardSleepType.Day, 50f, 12, 3f, "sling", new InventoryItem(BuiltinBlocks.SlingBullet), new InventoryItem(BuiltinBlocks.Sling)));

			CreateJob(new GuardJobSettings("guardbowdayjob", "pipliz.guardbowday", GuardJobSettings.EGuardSleepType.Night, 100f, 20, 5f, "bowShoot", new InventoryItem(BuiltinBlocks.BronzeArrow), new InventoryItem(BuiltinBlocks.Bow)));
			CreateJob(new GuardJobSettings("guardbownightjob", "pipliz.guardbownight", GuardJobSettings.EGuardSleepType.Day, 100f, 20, 5f, "bowShoot", new InventoryItem(BuiltinBlocks.BronzeArrow), new InventoryItem(BuiltinBlocks.Bow)));

			CreateJob(new GuardJobSettings("guardcrossbowdayjob", "pipliz.guardcrossbowday", GuardJobSettings.EGuardSleepType.Night, 300f, 25, 8f, "bowShoot", new InventoryItem(BuiltinBlocks.CrossbowBolt), new InventoryItem(BuiltinBlocks.Crossbow)));
			CreateJob(new GuardJobSettings("guardcrossbownightjob", "pipliz.guardcrossbownight", GuardJobSettings.EGuardSleepType.Day, 300f, 25, 8f, "bowShoot", new InventoryItem(BuiltinBlocks.CrossbowBolt), new InventoryItem(BuiltinBlocks.Crossbow)));

			GuardJobSettings settings = null;
			settings = new GuardJobSettings("guardmatchlockdayjob", "pipliz.guardmatchlockday", GuardJobSettings.EGuardSleepType.Night, 500f, 30, 12f, "matchlock", new InventoryItem(BuiltinBlocks.LeadBullet), new InventoryItem(BuiltinBlocks.MatchlockGun));
			settings.ShootItem.Add(new InventoryItem(BuiltinBlocks.GunpowderPouch));
			settings.OnShootResultItem = new ItemTypes.ItemTypeDrops(BuiltinBlocks.LinenPouch, 1, 0.9);
			CreateJob(settings);

			settings = new GuardJobSettings("guardmatchlocknightjob", "pipliz.guardmatchlocknight", GuardJobSettings.EGuardSleepType.Day, 500f, 30, 12f, "matchlock", new InventoryItem(BuiltinBlocks.LeadBullet), new InventoryItem(BuiltinBlocks.MatchlockGun));
			settings.ShootItem.Add(new InventoryItem(BuiltinBlocks.GunpowderPouch));
			settings.OnShootResultItem = new ItemTypes.ItemTypeDrops(BuiltinBlocks.LinenPouch, 1, 0.9);
			CreateJob(settings);

			// minerjob is special for both (need to save/load additional data in the instance, and custom stockpile/job behaviour)
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(
				new BlockJobManager<MinerJobSettings, MinerJobInstance>(
					new MinerJobSettings(),
					(setting, pos, type, bytedata) => new MinerJobInstance(setting, pos, type, bytedata),
					(setting, pos, type, colony) => new MinerJobInstance(setting, pos, type, colony)
				)
			);

			// scientistjob only has special ijobsettings (to deal with doing the science)
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(
				new BlockJobManager<ScientistJobSettings, BlockJobInstance>(
					new ScientistJobSettings(),
					(setting, pos, type, bytedata) => new BlockJobInstance(setting, pos, type, bytedata),
					(setting, pos, type, colony) => new BlockJobInstance(setting, pos, type, colony)
				)
			);

			ServerManager.BlockEntityCallbacks.RegisterEntityManager(
				new BlockJobManager<ConstructionJobSettings, ConstructionJobInstance>(
					new ConstructionJobSettings(),
					(setting, pos, type, bytedata) => new ConstructionJobInstance(setting, pos, type, bytedata),
					(setting, pos, type, colony) => new ConstructionJobInstance(setting, pos, type, colony)
				)
			);
		}

		static void CreateJob (GuardJobSettings settings)
		{
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(
				new BlockJobManager<GuardJobSettings, GuardJobInstance>(
					settings,
					(setting, pos, type, bytedata) => new GuardJobInstance(setting, pos, type, bytedata),
					(setting, pos, type, colony) => new GuardJobInstance(setting, pos, type, colony)
				)
			);
		}

		static void CreateJob (CraftingJobSettings settings)
		{
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(
				new BlockJobManager<CraftingJobSettings, CraftingJobInstance>(
					settings,
					(setting, pos, type, bytedata) => new CraftingJobInstance(setting, pos, type, bytedata),
					(setting, pos, type, colony) => new CraftingJobInstance(setting, pos, type, colony)
				)
			);
		}
	}
}
