using System;
using System.Collections.Generic;

namespace Pipliz.Mods.APIProvider.AreaJobs
{
	[AttributeUsage(AttributeTargets.Class)]
	[ModLoader.ModManager]
	public class AreaJobDefinitionAutoLoaderAttribute : Attribute
	{
		static List<Type> queuedDefinitions;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterModsLoaded, "pipliz.mods.apiprovider.areajobs.findattributes")]
		[ModLoader.ModDocumentation("Finds types marked with AreaJobDefinitionAutoLoaderAttribute")]
		static void FindAttributes (List<ModLoader.ModDescription> mods)
		{
			for (int i = 0; i < mods.Count; i++) {
				if (mods[i].HasAssembly) {
					Type[] types = mods[i].LoadedAssemblyTypes;
					for (int t = 0; t < types.Length; t++) {
						Type type = types[t];
						if (type.IsDefined(typeof(AreaJobDefinitionAutoLoaderAttribute), false) &&
							typeof(IAreaJobDefinition).IsAssignableFrom(type)) {
							if (queuedDefinitions == null) {
								queuedDefinitions = new List<Type>();
							}
							queuedDefinitions.Add(type);
						}
					}
				}
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.mods.apiprovider.areajobs.insertattributed")]
		[ModLoader.ModCallbackProvidesFor("pipliz.server.loadareajobs")]
		[ModLoader.ModDocumentation("Creates instance of registered IAreaJobDefinition and registers those to AreaJobTracker")]
		static void Load ()
		{
			if (queuedDefinitions != null) {
				for (int i = 0; i < queuedDefinitions.Count; i++) {
					try {
						AreaJobTracker.RegisterAreaJobDefinition((IAreaJobDefinition)Activator.CreateInstance(queuedDefinitions[i]));
					} catch (Exception e) {
						Log.WriteException(e);
					}
				}
				queuedDefinitions = null;
			}
		}
	}
}
