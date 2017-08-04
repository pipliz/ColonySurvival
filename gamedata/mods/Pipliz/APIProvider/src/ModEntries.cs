namespace Pipliz.APIProvider
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, "pipliz.apiprovider.jobs.registercallbacks")]
		public static void AfterWorldLoad ()
		{
			Jobs.BlockJobManagerTracker.RegisterCallback();
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, "pipliz.apiprovider.jobs.load")]
		public static void AfterWorldLoad2 ()
		{
			Jobs.BlockJobManagerTracker.Load();
		}

		[ModLoader.ModCallback (ModLoader.EModCallbackType.OnQuit, "pipliz.apiprovider.jobs.save")]
		public static void OnQuit ()
		{
			Jobs.BlockJobManagerTracker.Save();
		}

		[ModLoader.ModCallback (ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.apiprovider.registerrecipes")]
		public static void AfterItemTypesDefined ()
		{
			Jobs.BlockJobManagerTracker.RegisterRecipes();
		}
	}
}
