using System.Collections.Generic;

namespace Pipliz.Mods.APIProvider
{
	/// <summary>
	/// Contains the callback entries for this mod.
	/// Probably shouldn't call anything here manually.
	/// </summary>
	[ModLoader.ModManager]
	public static class ModEntries
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, "pipliz.apiprovider.jobs.registercallbacks")]
		[ModLoader.ModDocumentation("Registers callbacks for block job trackers")]
		public static void AfterWorldLoad ()
		{
			Jobs.BlockJobManagerTracker.RegisterCallback();
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad, "pipliz.apiprovider.jobs.load")]
		[ModLoader.ModDocumentation("Loads files for registered block job trackers")]
		public static void AfterWorldLoad2 ()
		{
			Jobs.BlockJobManagerTracker.Load();
		}

		[ModLoader.ModCallback (ModLoader.EModCallbackType.OnQuit, "pipliz.apiprovider.jobs.save")]
		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAutoSaveWorld, "pipliz.apiprovider.jobs.autosave")]
		[ModLoader.ModDocumentation("Saves files for registered block job trackers")]
		public static void OnQuit ()
		{
			Jobs.BlockJobManagerTracker.Save();
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.apiprovider.jobs.resolvetypes")]
		[ModLoader.ModCallbackDependsOn("pipliz.server.loadnpctypes")]
		[ModLoader.ModCallbackProvidesFor("pipliz.server.loadresearchables")]
		[ModLoader.ModDocumentation("Activates the blockjobmanagers, and registers block job provided npc types")]
		public static void AfterDefiningNPCTypes ()
		{
			Jobs.BlockJobManagerTracker.ResolveRegisteredTypes();
		}
	}
}
