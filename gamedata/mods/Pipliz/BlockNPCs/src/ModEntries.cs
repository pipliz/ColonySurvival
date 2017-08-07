using Pipliz.APIProvider.Jobs;
using Pipliz.APIProvider.Recipes;
using Pipliz.BlockNPCs.Implementations;
using System.Collections.Generic;
using System.IO;

namespace Pipliz.BlockNPCs
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		public static string ModDirectory;
		public static string ModGamedataDirectory;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "pipliz.blocknpcs.assemblyload")]
		public static void OnAssemblyLoaded (string path)
		{
			ModDirectory = Path.GetDirectoryName(path);
			ModGamedataDirectory = Path.Combine(ModDirectory, "gamedata/");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterDefiningNPCTypes, "pipliz.blocknpcs.registerjobs")]
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
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.blocknpcs.loadrecipes")]
		[ModLoader.ModCallbackProvidesFor ("pipliz.apiprovider.registerrecipes")]
		public static void AfterItemTypesDefined ()
		{
			RecipeManager.LoadRecipes("pipliz.tailor", Path.Combine(ModGamedataDirectory, "tailoring.json"));
			RecipeManager.LoadRecipes("pipliz.crafter", Path.Combine(ModGamedataDirectory, "crafting.json"));
			RecipeManager.LoadRecipes("pipliz.grinder", Path.Combine(ModGamedataDirectory, "grinding.json"));
			RecipeManager.LoadRecipes("pipliz.minter", Path.Combine(ModGamedataDirectory, "minting.json"));
			RecipeManager.LoadRecipes("pipliz.merchant", Path.Combine(ModGamedataDirectory, "shopping.json"));
			RecipeManager.LoadRecipes("pipliz.technologist", Path.Combine(ModGamedataDirectory, "technologist.json"));
			RecipeManager.LoadRecipesFueled("pipliz.smelter", Path.Combine(ModGamedataDirectory, "smelting.json"));
			RecipeManager.LoadRecipesFueled("pipliz.baker", Path.Combine(ModGamedataDirectory, "baking.json"));
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
