using BlockEntities;
using NPC;
using UnityEngine.Assertions;
using static NPC.NPCBase;

namespace Pipliz.APIProvider.Jobs
{
	public class BlockJobInstance : IJob, IBlockEntitySerializable, IBlockEntityKeepLoaded, IBlockEntityOnRemove
	{
		public virtual IBlockJobSettings Settings { get; set; }
		public virtual Vector3Int Position { get; set; }
		public virtual Colony Owner { get; set; }
		public virtual NPCBase NPC { get; set; }
		public virtual ItemTypes.ItemType BlockType { get; set; }
		public virtual NPCGoal LastNPCGoal { get; set; }
		public virtual bool ShouldTakeItems { get; set; }
		public virtual bool IsBusy { get; set; }
		public virtual bool IsValid { get; set; }

		public virtual NPCType NPCType { get { return Settings.NPCType; } }
		public virtual InventoryItem RecruitmentItem { get { return Settings.RecruitmentItem; } }
		public virtual bool NeedsNPC { get { return NPC == null; } }

		public BlockJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader)
		{
			Settings = settings;
			Position = position;
			BlockType = type;
			IsValid = true;

			int colonyID = (int)reader.ReadVariableUInt();
			int npcID = (int)reader.ReadVariableUInt();

			SetColonyAndNPC(ServerManager.ColonyTracker.Get(colonyID), npcID);
		}

		public BlockJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony)
		{
			Settings = settings;
			Position = position;
			BlockType = type;
			IsValid = true;

			SetColonyAndNPC(colony, 0);
		}

		public virtual void SetColonyAndNPC (Colony colony, int npcID)
		{
			Owner = colony;
			NPCBase foundNPC = null;
			if (npcID > 0 && !NPCTracker.TryGetNPC(npcID, out foundNPC)) {
				Log.WriteWarning("Failed to find npc ID {0}", npcID);
			}
			SetNPC(foundNPC, true, true);
		}

		public virtual ESerializeEntityResult SerializeToBytes (Vector3Int blockPosition, ByteBuilder builder)
		{
			Assert.IsTrue(IsValid);
			builder.WriteVariable((uint)Owner.ColonyID);
			builder.WriteVariable((uint)(NPC?.ID ?? 0));
			return ESerializeEntityResult.LoadChunkOnStartup | ESerializeEntityResult.WroteData;
		}

		// IBlockEntityKeepLoaded
		/// <summary>
		/// Ensures the crafting block's chunk is not unloaded as long as it exists
		/// </summary>
		public virtual EKeepChunkLoadedResult OnKeepChunkLoaded (Vector3Int blockPosition)
		{
			Assert.IsTrue(IsValid);
			return EKeepChunkLoadedResult.YesLong;
		}

		public virtual NPCGoal CalculateGoal (ref NPCState state)
		{
			Assert.IsTrue(IsValid);
			NPCGoal newGoal;
			if (Settings.ToSleep) {
				newGoal = state.Inventory.IsEmpty ? NPCGoal.Bed : NPCGoal.Stockpile;
			} else {
				newGoal = ShouldTakeItems ? NPCGoal.Stockpile : NPCGoal.Job;
			}
			if (newGoal != LastNPCGoal) {
				Settings.OnGoalChanged(this, LastNPCGoal, newGoal);
				LastNPCGoal = newGoal;
			}
			return newGoal;
		}

		public virtual void SetNPC (NPCBase npc)
		{
			SetNPC(npc, false, false);
		}

		public virtual void SetNPC (NPCBase npc, bool isLoading, bool callTakeJob)
		{
			Assert.IsTrue(IsValid);
			// remove job from current npc if exists
			// queue for job tracker if npc == null
			if (NPC != npc) {
				if (!isLoading) {
					World.GetChunk(Position.ToChunk()).IsDirty = true;
				}
				if (ThreadManager.IsMainThread) {
					NPC?.ClearJob();
					NPC = npc;
					if (NPC == null) {
						Owner.JobFinder.Add(this);
					} else {
						if (callTakeJob) {
							NPC.TakeJob(this);
						}
					}
				} else {
					if (NPC != null) {
						ThreadManager.InvokeOnMainThread(NPC.ClearJob);
					}
					NPC = npc;
					if (NPC == null) {
						ThreadManager.InvokeOnMainThread(() => Owner.JobFinder.Add(this));
					} else {
						if (callTakeJob) {
							ThreadManager.InvokeOnMainThread(() => NPC.TakeJob(this));
						}
					}
				}
			} else if (npc == null) {
				if (ThreadManager.IsMainThread) {
					Owner.JobFinder.Add(this);
				} else {
					ThreadManager.InvokeOnMainThread(() => Owner.JobFinder.Add(this));
				}
			}
		}

		public virtual void OnRemove (Vector3Int blockPosition)
		{
			Owner.JobFinder.Remove(this);
			IsValid = false;
		}

		public virtual Vector3Int GetJobLocation ()
		{
			Assert.IsTrue(IsValid);
			return Settings.GetJobLocation(this);
		}

		public virtual void OnNPCAtJob (ref NPCState state)
		{
			Assert.IsTrue(IsValid);
			Settings.OnNPCAtJob(this, ref state);
		}

		public virtual void OnNPCAtStockpile (ref NPCState state)
		{
			Assert.IsTrue(IsValid);
			Settings.OnNPCAtStockpile(this, ref state);
		}
	}
}
