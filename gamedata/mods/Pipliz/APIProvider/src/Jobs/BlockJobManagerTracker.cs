using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipliz.Mods.APIProvider.Jobs
{
	/// <remarks>
	/// Sorry for creating a ManagerTracker :)
	/// </remarks>
	public static class BlockJobManagerTracker
	{
		static List<IBlockJobManager> InstanceList = new List<IBlockJobManager>();

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
				try {
					pair.Value.Invoke(pair.Key);
				} catch (Exception e) {
					Log.WriteException("Error resolving blockjob {0}:", e, pair.Key);
				}
			}
			RegisteredTypes = null;
		}

		/// <summary>
		/// Directly resolve a type, bypassing the register stage. NPC's registered like this can't be reasonably altered.
		/// </summary>
		public static void Resolve<T> (string blockName) where T : ITrackableBlock, IBlockJobBase, INPCTypeDefiner, new()
		{
			var instance = new T();
			Server.NPCs.NPCType.AddSettings(instance.GetNPCTypeDefinition());
			InstanceList.Add(new BlockJobManager<T>(blockName));
		}

		public static void RemoveBlockTypeAt (Type t, Vector3Int position)
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				if (InstanceList[i].GetType().GetGenericArguments()[0].Equals(t)) {
					InstanceList[i].OnRemove(position, 0, null);
					return;
				}
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
		public static void RegisterCallback ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				try {
					InstanceList[i].RegisterCallback();
				} catch (Exception e) {
					Log.WriteException("Error registering APIProvider callbacks of blockjob {0}:", e, InstanceList[i].ToString());
				}
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
		public static void Load ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				try {
					InstanceList[i].Load();
				} catch (Exception e) {
					Log.WriteException("Error loading APIProvider blockjob {0}:", e, InstanceList[i].ToString());
				}
			}
		}

		/// <summary>
		/// Called by APIProvider ModEntries
		/// </summary>
		public static void Save ()
		{
			if (ServerManager.WorldName == null) {
				return;
			}
			for (int i = 0; i < InstanceList.Count; i++) {
				try {
					InstanceList[i].OnSave();
				} catch (Exception e) {
					Log.WriteException("Error saving APIProvider blockjob {0}:", e, InstanceList[i].ToString());
				}
			}
		}
	}
}
