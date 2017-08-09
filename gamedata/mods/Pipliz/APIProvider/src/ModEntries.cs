namespace Pipliz.APIProvider
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		/// <summary>
		/// Register npc's in a callback of AfterDefiningNPCTypes that provides for this one.
		/// Probably not to be called manually.
		/// </summary>
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterDefiningNPCTypes, "pipliz.apiprovider.jobs.resolvetypes")]
		public static void AfterDefiningNPCTypes ()
		{
			Jobs.BlockJobManagerTracker.ResolveRegisteredTypes();
		}

		/// <summary>
		/// Probably not to be called manually.
		/// </summary>
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, "pipliz.apiprovider.jobs.registercallbacks")]
		public static void AfterWorldLoad ()
		{
			Jobs.BlockJobManagerTracker.RegisterCallback();
		}

		/// <summary>
		/// Probably not to be called manually.
		/// </summary>
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, "pipliz.apiprovider.jobs.load")]
		public static void AfterWorldLoad2 ()
		{
			Jobs.BlockJobManagerTracker.Load();
		}

		/// <summary>
		/// Probably not to be called manually.
		/// </summary>
		[ModLoader.ModCallback (ModLoader.EModCallbackType.OnQuit, "pipliz.apiprovider.jobs.save")]
		public static void OnQuit ()
		{
			Jobs.BlockJobManagerTracker.Save();
		}

		/// <summary>
		/// Probably not to be called manually.
		/// </summary>
		[ModLoader.ModCallback (ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.apiprovider.registerrecipes")]
		public static void AfterItemTypesDefined ()
		{
			Jobs.BlockJobManagerTracker.RegisterRecipes();
		}
	}
}
