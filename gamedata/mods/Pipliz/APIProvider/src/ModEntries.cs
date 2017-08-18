using System.Collections.Generic;

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

		/// <summary>
		/// Parses all loaded mod assemblies, searching for types tagged with [AutoLoadedResearchable]
		/// Registers those types to the game
		/// </summary>
		/// <param name="assemblies"></param>
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterModsLoaded, "pipliz.apiprovider.parsemods")]
		public static void AfterModsLoaded (List<ModLoader.ModAssembly> assemblies)
		{
			foreach (var modAssembly in assemblies) {
				try {
					foreach (var type in modAssembly.Assembly.GetTypes()) {
						try {
							object[] attributes = type.GetCustomAttributes(typeof(Science.AutoLoadedResearchableAttribute), true);
							if (attributes != null && attributes.Length > 0) {
								for (int i = 0; i < attributes.Length; i++) {
									Science.AutoLoadedResearchableAttribute attri = attributes[i] as Science.AutoLoadedResearchableAttribute;
									if (attri != null) {
										Science.ResearchableManager.Add(type);
									}
								}
							}
						} catch (System.Exception e) {
							Log.WriteException("APIProvider threw exception parsing dll {0}, type {1}", e, System.IO.Path.GetFileName(modAssembly.DllPath), type.FullName);
						}
					}
				} catch (System.Exception e) {
					Log.WriteException("APIProvider threw exception parsing dll {0}", e, System.IO.Path.GetFileName(modAssembly.DllPath));
				}
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAddResearchables, "pipliz.apiprovider.registerautoresearchables")]
		public static void RegisterAutoResearchables ()
		{
			Science.ResearchableManager.Register();
		}
	}
}
