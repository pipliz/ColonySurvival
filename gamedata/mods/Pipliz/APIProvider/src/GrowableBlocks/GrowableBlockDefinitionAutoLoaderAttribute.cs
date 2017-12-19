using Server.GrowableBlocks;
using System;
using System.Collections.Generic;

namespace Pipliz.Mods.APIProvider.GrowableBlocks
{
	[AttributeUsage(AttributeTargets.Class)]
	[ModLoader.ModManager]
	public class GrowableBlockDefinitionAutoLoaderAttribute : Attribute
	{
		static List<Type> queuedDefinitions;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterModsLoaded, "pipliz.mods.apiprovider.growableblocks.findattributes")]
		static void FindAttributes (List<ModLoader.ModDescription> mods)
		{
			for (int i = 0; i < mods.Count; i++) {
				if (mods[i].HasAssembly) {
					Type[] types = mods[i].LoadedAssemblyTypes;
					for (int t = 0; t < types.Length; t++) {
						Type type = types[t];
						if (type.IsDefined(typeof(GrowableBlockDefinitionAutoLoaderAttribute), false) &&
							typeof(IGrowableBlockDefinition).IsAssignableFrom(type)) {
							if (queuedDefinitions == null) {
								queuedDefinitions = new List<Type>();
							}
							queuedDefinitions.Add(type);
						}
					}
				}
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "pipliz.mods.apiprovider.growableblocks.insertattributed")]
		[ModLoader.ModCallbackProvidesFor("pipliz.server.growableblocks.loadblocks")]
		static void Load ()
		{
			if (queuedDefinitions != null) {
				for (int i = 0; i < queuedDefinitions.Count; i++) {
					try {
						GrowableBlockManager.RegisterGrowableBlockType((IGrowableBlockDefinition)Activator.CreateInstance(queuedDefinitions[i]));
					} catch (Exception e) {
						Log.WriteException(e);
					}
				}
				queuedDefinitions = null;
			}
		}
	}
}
