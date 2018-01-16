using NPC;
using Pipliz.Collections;
using Pipliz.Helpers;
using Server.NPCs;
using System.Threading;

namespace Pipliz.Mods.APIProvider.AreaJobs
{
	using JSON;

	public class AreaJobDefinitionDefault<T> : IAreaJobDefinition where T : IAreaJobDefinition
	{
		protected string fileName;
		protected string identifier;
		protected ushort[] stages;
		protected NPCType npcType;

		protected SortedList<Players.Player, JSONNode> SavedJobs;

		protected JSONNode LoadedRoot;
		protected ManualResetEvent FinishedLoadingEvent = new ManualResetEvent(false);

		public virtual NPCType UsedNPCType { get { return npcType; } }

		public virtual string Identifier { get { return identifier; } }

		public virtual string FilePath { get { return string.Format("gamedata/savegames/{0}/areajobs/{1}.json", ServerManager.WorldName, fileName); } }

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
			int npcID = node.GetAsOrDefault("npcID", 0);
			return CreateAreaJob(owner, min, max, npcID);
		}

		public virtual IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			return new DefaultFarmerAreaJob<T>(owner, min, max, npcID);
		}

		public virtual void OnRemove (IAreaJob job)
		{

		}

		public virtual void CalculateSubPosition (IAreaJob job, ref Vector3Int positionSub)
		{
			if (stages == null || stages.Length < 2) {
				return;
			}

			bool hasSeeds = job.UsedNPC.Colony.UsedStockpile.Contains(stages[0]);
			bool reversed = false;
			Vector3Int firstPlanting = Vector3Int.invalidPos;
			Vector3Int min = job.Minimum;
			Vector3Int max = job.Maximum;

			for (int x = min.x; x <= max.x; x++) {
				int z = reversed ? max.z : min.z;
				while (reversed ? (z >= min.z) : (z <= max.z)) {

					ushort type;
					Vector3Int possiblePositionSub = new Vector3Int(x, min.y, z);
					if (!Server.AI.AIManager.Loaded(possiblePositionSub) || !World.TryGetTypeAt(possiblePositionSub, out type)) {
						return;
					}
					if (type == 0) {
						if (!hasSeeds && !firstPlanting.IsValid) {
							firstPlanting = possiblePositionSub;
						}
						if (hasSeeds) {
							positionSub = possiblePositionSub;
							return;
						}
					}
					if (type == stages[stages.Length - 1]) {
						positionSub = possiblePositionSub;
						return;
					}

					z = reversed ? z - 1 : z + 1;
				}
				reversed = !reversed;
			}

			if (firstPlanting.IsValid) {
				positionSub = firstPlanting;
				return;
			}

			int xRandom = Random.Next(min.x, max.x + 1);
			int zRandom = Random.Next(min.z, max.z + 1);
			positionSub = new Vector3Int(xRandom, min.y, zRandom);
		}

		public virtual void OnNPCAtJob (IAreaJob job, ref Vector3Int positionSub, ref NPCBase.NPCState state, ref bool shouldDumpInventory)
		{
			if (stages == null || stages.Length < 2) {
				state.SetCooldown(1.0);
				return;
			}
			state.JobIsDone = true;
			if (positionSub.IsValid) {
				ushort type;
				if (World.TryGetTypeAt(positionSub, out type)) {
					ushort typeSeeds = stages[0];
					ushort typeFinal = stages[stages.Length - 1];
					if (type == 0) {
						if (state.Inventory.TryGetOneItem(typeSeeds)
							|| job.UsedNPC.Colony.UsedStockpile.TryRemove(typeSeeds)) {
							ServerManager.TryChangeBlock(positionSub, typeSeeds, ServerManager.SetBlockFlags.DefaultAudio);
							state.SetCooldown(1.0);
							shouldDumpInventory = false;
						} else {
							state.SetIndicator(NPCIndicatorType.MissingItem, 2f, typeSeeds);
							shouldDumpInventory = state.Inventory.UsedCapacity > 0f;
						}
					} else if (type == typeFinal) {
						if (ServerManager.TryChangeBlock(positionSub, 0, ServerManager.SetBlockFlags.DefaultAudio)) {
							job.UsedNPC.Inventory.Add(ItemTypes.GetType(typeFinal).OnRemoveItems);
						}
						state.SetCooldown(1.0);
						shouldDumpInventory = false;
					} else {
						shouldDumpInventory = state.Inventory.UsedCapacity > 0f;

						Server.GrowableBlocks.IGrowableBlock block;
						if (Server.GrowableBlocks.GrowableBlockManager.TryGetGrowableBlock(positionSub, out block)) {
							state.SetCooldown(5.0);
						} else {
							bool found = false;
							for (int i = 0; i < stages.Length; i++) {
								if (stages[i] == type) {
									ItemTypesServer.OnChange(positionSub, 0, type, null);
									state.SetIndicator(NPCIndicatorType.Crafted, 2f, type);
									state.SetCooldown(0.2);
									found = true;
									break;
								}
							}
							if (!found) {
								state.SetCooldown(5.0);
							}
						}
					}
				} else {
					state.SetCooldown(Random.NextFloat(3f, 6f));
				}
				positionSub = Vector3Int.invalidPos;
			} else {
				state.SetCooldown(10.0);
			}
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
				Log.Write("Waiting for {0} to finish loading...", typeof(T));
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
					if (!AreaJobTracker.RegisterAreaJob(job)) {
						job.OnRemove();
					}
				}
			}
		}

		public virtual void SaveJob (Players.Player owner, JSONNode data)
		{
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
					Log.Write("Saving {0}", fileName);
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

		protected void SetLayer (Vector3Int min, Vector3Int max, ushort type, int layer)
		{
			int yLayer = min.y + layer;
			for (int x = min.x; x <= max.x; x++) {
				for (int z = min.z; z <= max.z; z++) {
					ServerManager.TryChangeBlock(new Vector3Int(x, yLayer, z), type);
				}
			}
		}
	}
}
