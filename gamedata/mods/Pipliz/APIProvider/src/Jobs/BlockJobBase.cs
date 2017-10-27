using NPC;
using Pipliz.JSON;
using Server.NPCs;

namespace Pipliz.APIProvider.Jobs
{
	public class BlockJobBase : IJob, ITrackableBlock
	{
		protected NPCBase usedNPC;
		protected Vector3Int position;
		protected Players.Player owner;

		double timeJob;
		bool isValid = true;
		NPCType cached_NPCType;
		protected NPCBase.NPCGoal lastGoal;

		public virtual Vector3Int KeyLocation { get { return position; } }

		public virtual bool IsValid { get { return isValid; } }

		public virtual Players.Player Owner { get { return owner; } }

		// excuse me for this, it's a stub needed for the area jobs atm
		public void CopyData (ByteBuilder b) { }

		public virtual NPCType NPCType
		{
			get
			{
				if (!cached_NPCType.IsValid) {
					cached_NPCType = NPCType.GetByKeyNameOrDefault(NPCTypeKey);
				}
				return cached_NPCType;
			}
		}

		public virtual bool NeedsNPC { get { return usedNPC == null || !usedNPC.IsValid; } }

		public virtual bool NeedsItems { get { return false; } }

		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }

		public virtual string NPCTypeKey { get { throw new System.NotImplementedException(); } }

		public virtual float TimeBetweenJobs { get { throw new System.NotImplementedException(); } }

		public virtual InventoryItem RecruitementItem { get { return InventoryItem.Empty; } }

		public virtual void InitializeJob (Players.Player owner, Vector3Int position, int desiredNPCID)
		{
			this.position = position;
			this.owner = owner;

			if (desiredNPCID != 0) {
				if (NPCTracker.TryGetNPC(desiredNPCID, out usedNPC)) {
					usedNPC.TakeJob(this);
				} else {
					Log.WriteWarning("Failed to find npc ID {0}", desiredNPCID);
				}
			}
			if (usedNPC == null) {
				JobTracker.Add(this);
			}
		}

		public virtual void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			if (CheckTime()) {
				OnNPCDoJob(ref state);
			}
		}

		public virtual void OnNPCAtStockpile (ref NPCBase.NPCState state)
		{
			if (CheckTime()) {
				OnNPCDoStockpile(ref state);
			}
		}

		public virtual void OnAssignedNPC (NPCBase npc)
		{
			usedNPC = npc;
		}

		public virtual void OnRemovedNPC ()
		{
			usedNPC = null;
			JobTracker.Add(this);
		}
		
		public virtual void OnRemove ()
		{
			isValid = false;
			if (usedNPC != null) {
				usedNPC.ClearJob();
				usedNPC = null;
			}
			JobTracker.Remove(owner, KeyLocation);
		}

		public virtual NPCBase.NPCGoal CalculateGoal (ref NPCBase.NPCState state)
		{
			NPCBase.NPCGoal newGoal;
			if (ToSleep) {
				if (!state.Inventory.IsEmpty) {
					newGoal = NPCBase.NPCGoal.Stockpile;
				} else {
					newGoal = NPCBase.NPCGoal.Bed;
				}
			} else {
				if (state.Inventory.Full || NeedsItems) {
					newGoal = NPCBase.NPCGoal.Stockpile;
				} else {
					newGoal = NPCBase.NPCGoal.Job;
				}
			}
			if (lastGoal != newGoal) {
				OnChangedGoal(lastGoal, newGoal);
				lastGoal = newGoal;
			}
			return newGoal;
		}

		public virtual JSONNode GetJSON ()
		{
			return new JSONNode()
				.SetAs("npcID", usedNPC == null ? 0 : usedNPC.ID)
				.SetAs("position", (JSONNode)position);
		}

		public virtual Vector3Int GetJobLocation ()
		{
			return KeyLocation;
		}

		public virtual ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			return this;
		}

		public virtual void OnNPCDoJob (ref NPCBase.NPCState state)
		{

		}

		public virtual void OnNPCDoStockpile (ref NPCBase.NPCState state)
		{
			state.Inventory.TryDump(usedNPC.Colony.UsedStockpile);
			state.SetCooldown(0.1);
		}

		protected void OverrideCooldown (double cooldownLeft)
		{
			timeJob = Time.SecondsSinceStartDouble + cooldownLeft;
		}

		protected virtual void OnChangedGoal (NPCBase.NPCGoal oldGoal, NPCBase.NPCGoal newGoal)
		{

		}

		protected virtual bool CheckTime ()
		{
			double timeNow = Time.SecondsSinceStartDouble;
			if (timeNow < timeJob) {
				return false;
			}
			timeJob = timeNow + (Random.NextFloat() * 0.2f + 1.0f) * TimeBetweenJobs;
			return true;
		}
	}
}
