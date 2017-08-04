using System.Collections.Generic;
using System.Linq;

namespace Pipliz.APIProvider.Jobs
{
	/// <remarks>
	/// Sorry for creating a ManagerTracker :)
	/// </remarks>
	public static class BlockJobManagerTracker
	{
		static List<IBlockJobManager> InstanceList = new List<IBlockJobManager>();
		static List<KeyValuePair<string, IRecipeLimitsProvider>> LimitsProviders = new List<KeyValuePair<string, IRecipeLimitsProvider>>();

		public static void Register<T> (string blockName) where T : ITrackableBlock, IBlockJobBase, INPCTypeDefiner, new()
		{
			var instance = new T();
			NPC.NPCType.AddSettings(instance.GetNPCTypeDefinition());
			if (typeof(IRecipeLimitsProvider).IsAssignableFrom(typeof(T))) {
				LimitsProviders.Add(new KeyValuePair<string, IRecipeLimitsProvider>(blockName, (IRecipeLimitsProvider)instance));
			}
			InstanceList.Add(new BlockJobManager<T>(blockName));
		}

		public static void RegisterCallback ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].RegisterCallback();
			}
		}

		public static void Load ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].Load();
			}
		}

		public static void Save ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].OnSave();
			}
		}

		public static void RegisterRecipes ()
		{
			for (int i = 0; i < LimitsProviders.Count; i++) {
				var recipeLimitsProvider = LimitsProviders[i].Value;
				var list = recipeLimitsProvider.GetCraftingLimitsRecipes();
				if (list != null) {
					RecipeLimits.SetRecipes(recipeLimitsProvider.GetCraftingLimitsIdentifier(), list.ToList());
					var triggers = recipeLimitsProvider.GetCraftingLimitsTriggers();
					if (triggers == null) {
						RecipeLimits.SetInterface(LimitsProviders[i].Key, recipeLimitsProvider.GetCraftingLimitsIdentifier());
					} else {
						for (int i2 = 0; i2 < triggers.Count; i2++) {
							RecipeLimits.SetInterface(triggers[i2], recipeLimitsProvider.GetCraftingLimitsIdentifier());
						}
					}
				}
			}
		}
	}
}
