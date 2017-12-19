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
		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAutoSaveWorld, "pipliz.apiprovider.jobs.autosave")]
		public static void OnQuit ()
		{
			Jobs.BlockJobManagerTracker.Save();
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.apiprovider.jobs.resolvetypes")]
		[ModLoader.ModCallbackDependsOn("pipliz.server.loadnpctypes")]
		[ModLoader.ModCallbackProvidesFor("pipliz.apiprovider.registerrecipes")]
		public static void AfterDefiningNPCTypes ()
		{
			Jobs.BlockJobManagerTracker.ResolveRegisteredTypes();
		}

		[ModLoader.ModCallback (ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.apiprovider.registerrecipes")]
		[ModLoader.ModCallbackProvidesFor("pipliz.server.loadresearchables")]
		public static void AfterItemTypesDefined ()
		{
			Jobs.BlockJobManagerTracker.RegisterRecipes();
		}

		/// <summary>
		/// Parses all loaded mod assemblies, searching for types tagged with [AutoLoadedResearchable]
		/// Registers those types to the game
		/// </summary>
		/// <param name="assemblies"></param>
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterModsLoaded, "pipliz.apiprovider.parsemods")]
		public static void AfterModsLoaded (List<ModLoader.ModDescription> assemblies)
		{
			foreach (var modAssembly in assemblies) {
				if (modAssembly.HasAssembly) {
					foreach (System.Type type in modAssembly.LoadedAssemblyTypes) {
						try {
							if (type.IsDefined(typeof(Science.AutoLoadedResearchableAttribute), true)) {
								Science.ResearchableManager.Add(type);
							}
						} catch (System.Exception e) {
							Log.WriteException("APIProvider threw exception parsing dll {0}, type {1}", e, System.IO.Path.GetFileName(modAssembly.LoadedAssembly.Location), type.FullName);
						}
					}
				}
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAddResearchables, "pipliz.apiprovider.registerresearchables")]
		public static void RegisterAutoResearchables ()
		{
			Science.ResearchableManager.Register();
		}
	}
}
