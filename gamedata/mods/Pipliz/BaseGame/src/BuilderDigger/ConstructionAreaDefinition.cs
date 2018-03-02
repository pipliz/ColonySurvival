namespace Pipliz.Mods.BaseGame.Construction
{
	using APIProvider.AreaJobs;
	using Collections;
	using Helpers;
	using JSON;
	using NPC;
	using Server.NPCs;
	using System.Threading;
	using Shared;
	using System;

	[AreaJobDefinitionAutoLoader]
	public class ConstructionAreaDefinition : IAreaJobDefinition
	{
		protected NPCType npcType = NPCType.GetByKeyNameOrDefault("pipliz.constructor");

		protected SortedList<Players.Player, JSONNode> SavedJobs;

		protected JSONNode LoadedRoot;
		protected ManualResetEvent FinishedLoadingEvent = new ManualResetEvent(false);

		public virtual NPCType UsedNPCType { get { return npcType; } }

		public virtual string Identifier { get { return "pipliz.constructionarea"; } }

		public virtual string FilePath { get { return string.Format("gamedata/savegames/{0}/areajobs/constructionareas.json", ServerManager.WorldName); } }

		public virtual EAreaType AreaType { get { throw new NotImplementedException(); } }

		public virtual IAreaJob CreateAreaJob (Players.Player owner, JSONNode node)
		{
			Vector3Int min = Vector3Int.invalidPos;
			Vector3Int max = Vector3Int.invalidPos;

			JSONNode child;
			if (node.TryGetChild("min", out child)) {
				min.x = child.GetAsOrDefault("x", -1);
				min.y = child.GetAsOrDefault("y", -1);
				min.z = child.GetAsOrDefault("z", -1);
			}
			if (node.TryGetChild("max", out child)) {
				max.x = child.GetAsOrDefault("x", -1);
				max.y = child.GetAsOrDefault("y", -1);
				max.z = child.GetAsOrDefault("z", -1);
			}
			JSONNode args;
			if (node.TryGetChild("arguments", out args)) {
				if (args.NodeType == NodeType.Value && args.GetAs<string>() == "none") {
					return null;
				}
			}
			ConstructionArea area = (ConstructionArea)CreateAreaJob(owner, min, max);
			area.SetArgument(args);
			return area;
		}

		public virtual IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			return new ConstructionArea(owner, min, max);
		}

		public virtual void OnRemove (IAreaJob job)
		{
		}

		public virtual void CalculateSubPosition (IAreaJob job, ref Vector3Int positionSub)
		{
		}

		public virtual void OnNPCAtJob (IAreaJob job, ref Vector3Int positionSub, ref NPCBase.NPCState state, ref bool shouldDumpInventory)
		{
		}

		public virtual void StartLoading ()
		{
			ThreadPool.QueueUserWorkItem(AsyncLoad);
		}

		protected virtual void AsyncLoad (object obj)
		{
			try {
				JSON.Deserialize(FilePath, out LoadedRoot, false);
			} catch (System.Exception e) {
				Log.WriteException(e);
			} finally {
				FinishedLoadingEvent.Set();
			}
		}

		public virtual void FinishLoading ()
		{
			while (!FinishedLoadingEvent.WaitOne(500)) {
				Log.Write("Waiting for construction areas to finish loading...");
			}
			FinishedLoadingEvent = null;
			if (LoadedRoot != null) {
				LoadJSON(LoadedRoot);
				LoadedRoot = null;
			}
		}

		public virtual void LoadJSON (JSONNode node)
		{
			JSONNode table = node.GetAs<JSONNode>("table");
			foreach (var pair in table.LoopObject()) {
				Players.Player player = Players.GetPlayer(NetworkID.Parse(pair.Key));
				JSONNode array = pair.Value;
				for (int i = 0; i < array.ChildCount; i++) {
					var job = CreateAreaJob(player, array[i]);
					if (job == null) {
						continue;
					}
					if (!AreaJobTracker.RegisterAreaJob(job)) {
						job.OnRemove();
					}
				}
			}
		}

		public virtual void SaveJob (Players.Player owner, JSONNode data)
		{
			Log.Write("Saving a construction area");
			if (SavedJobs == null) {
				SavedJobs = new SortedList<Players.Player, JSONNode>(10);
			}
			JSONNode array;
			if (!SavedJobs.TryGetValue(owner, out array)) {
				array = new JSONNode(NodeType.Array);
				SavedJobs[owner] = array;
			}
			array.AddToArray(data);
		}

		public virtual void FinishSaving ()
		{
			General.Application.StartAsyncQuitToComplete(delegate ()
			{
				if (General.Application.IsQuiting) {
					Log.Write("Saving {0} construction areas", SavedJobs == null ? 0 : SavedJobs.Count);
				}

				JSONNode root = new JSONNode();
				root.SetAs("version", 0);
				JSONNode table = new JSONNode();
				root.SetAs("table", table);
				if (SavedJobs != null) {
					for (int i = 0; i < SavedJobs.Count; i++) {
						Players.Player p = SavedJobs.GetKeyAtIndex(i);
						JSONNode n = SavedJobs.GetValueAtIndex(i);
						table.SetAs(p.IDString, n);
					}
					SavedJobs = null;
				}

				string filePath = FilePath;
				IOHelper.CreateDirectoryFromFile(filePath);
				JSON.Serialize(filePath, root, 3);
			});
		}
	}
}