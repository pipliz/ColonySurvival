using System;
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

		static Dictionary<string, Action<string>> RegisteredTypes = new Dictionary<string, Action<string>>();

		/// <summary>
		/// Registers a npc type to be Resolved into the game @ ResolveRegisteredTypes.
		/// Allows for removing/replacing types inbetween Register and ResolveRegisteredTypes.
		/// </summary>
		public static void Register<T> (string blockName) where T : ITrackableBlock, IBlockJobBase, INPCTypeDefiner, new()
		{
			RegisteredTypes[blockName] = delegate (string str)
			{
				Resolve<T>(str);
			};
		}

		/// <summary>
		/// Call inbetween register and resolveTypes. Removes the npc attached to the block
		/// </summary>
		/// <returns>
		/// Returns true if succesful (i.e, there was one registered)
		/// </returns>
		public static bool ClearType (string blockName)
		{
			return RegisteredTypes.Remove(blockName);
		}

		/// <summary>
		/// Returns enumerable to go through the registered block keys.
		/// </summary>
		public static IEnumerable<string> RegisteredTypesEnumerable { get { return RegisteredTypes.Keys.AsEnumerable(); } }

		/// <summary>
		/// Resolves the registered types to the game
		/// </summary>
		public static void ResolveRegisteredTypes ()
		{
			foreach (var pair in RegisteredTypes) {
				pair.Value.Invoke(pair.Key);
			}
		}

		/// <summary>
		/// Directly resolve a type, bypassing the register stage. NPC's registered like this can't be reasonably altered.
		/// </summary>
		public static void Resolve<T> (string blockName) where T : ITrackableBlock, IBlockJobBase, INPCTypeDefiner, new()
		{
			foreach (var pair in RegisteredTypes) {
				var instance = new T();
				NPC.NPCType.AddSettings(instance.GetNPCTypeDefinition());
				if (typeof(IRecipeLimitsProvider).IsAssignableFrom(typeof(T))) {
					LimitsProviders.Add(new KeyValuePair<string, IRecipeLimitsProvider>(blockName, (IRecipeLimitsProvider)instance));
				}
				InstanceList.Add(new BlockJobManager<T>(blockName));
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
		public static void RegisterCallback ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].RegisterCallback();
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
		public static void Load ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].Load();
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
		public static void Save ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].OnSave();
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
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
