using NPC;
using System.Collections.Generic;

namespace Pipliz.BlockNPCs
{
	public class BlockJobManager<T> : IBlockJobManager where T : IBlockJobBase, ITrackableBlock, new()
	{
		BlockTracker tracker;
		string blockName;

		public BlockJobManager (string blockName)
		{
			this.blockName = blockName;
			tracker = new BlockTracker(blockName);
		}

		public void RegisterCallbackAndLoad ()
		{
			ItemTypesServer.RegisterOnAdd(blockName, OnAdd);
			ItemTypesServer.RegisterOnRemove(blockName, OnRemove);
			tracker.Load<T>();
		}

		public void OnSave ()
		{
			tracker.Save();
		}

		void OnRemove (Vector3Int position, ushort type, Players.Player player)
		{
			tracker.Remove(position);
		}

		void OnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			tracker.Add(new T().InitializeOnAdd(position, type, player));
		}
	}

	/// <remarks>
	/// Sorry for creating a ManagerTracker :)
	/// </remarks>
	public static class BlockJobManagerTracker
	{
		static List<IBlockJobManager> InstanceList = new List<IBlockJobManager>();

		public static void Register<T> (string blockName) where T : ITrackableBlock, IBlockJobBase, INPCTypeDefiner, new()
		{
			NPCType.AddSettings(new T().GetNPCTypeDefinition());
			InstanceList.Add(new BlockJobManager<T>(blockName));
		}

		public static void AfterWorldLoad ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].RegisterCallbackAndLoad();
			}
		}

		public static void Save ()
		{
			for (int i = 0; i < InstanceList.Count; i++) {
				InstanceList[i].OnSave();
			}
		}
	}

	public interface IBlockJobManager
	{
		void RegisterCallbackAndLoad ();
		void OnSave ();
	}
}
