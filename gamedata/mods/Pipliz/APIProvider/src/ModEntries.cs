namespace Pipliz.APIProvider
{
	/// <summary>
	/// Contains the callback entries for this mod.
	/// Probably shouldn't call anything here manually.
	/// </summary>
	[ModLoader.ModManager]
	public static class ModEntries
	{
		/// <summary>
		/// Register npc's in a callback of AfterDefiningNPCTypes that provides for this one.
		/// </summary>
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterDefiningNPCTypes, "pipliz.apiprovider.jobs.resolvetypes")]
		public static void AfterDefiningNPCTypes ()
		{
			Jobs.BlockJobManagerTracker.ResolveRegisteredTypes();
		}

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
