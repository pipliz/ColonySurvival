using NPC;
using Pipliz.JSON;
using Server.NPCs;

namespace Pipliz.Mods.APIProvider.Jobs
{
	public class BlockJobBase : IJob, ITrackableBlock
	{
		protected NPCBase usedNPC;
		protected Vector3Int position;
		protected Players.Player owner;

		protected bool isValid = true;
		protected bool worldTypeChecked = false;
		protected ushort worldType = 0;

		NPCType cached_NPCType;
		protected NPCBase.NPCGoal lastGoal;

		public virtual Vector3Int KeyLocation { get { return position; } }

		public virtual NPCBase NPC
		{
			get { return usedNPC; }
			set
			{
				if (usedNPC != value) {
					if (usedNPC != null) {
						usedNPC.ClearJob();
					}
					usedNPC = value;
					if (usedNPC == null) {
						JobTracker.Add(this);
					} else {
						usedNPC.TakeJob(this);
					}
				} else if (value == null) {
					JobTracker.Add(this);
				}
			}
		}

		public virtual bool IsValid
		{
			get
			{
				if (isValid && !worldTypeChecked) {
					CheckWorldType();
				}
				return isValid;
			}
		}

		public virtual Players.Player Owner { get { return owner; } }

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

		public virtual InventoryItem RecruitementItem { get { return InventoryItem.Empty; } }

		public virtual void InitializeJob (Players.Player owner, Vector3Int position, int desiredNPCID)
		{
			this.position = position;
			this.owner = owner;

			NPCBase foundNPC = null;

			if (desiredNPCID != 0) {
				if (!NPCTracker.TryGetNPC(desiredNPCID, out foundNPC)) {
					Log.WriteWarning("Failed to find npc ID {0}", desiredNPCID);
				}
			}
			NPC = foundNPC;
		}

		public virtual void OnNPCAtJob (ref NPCBase.NPCState state)
		{

		}

		public virtual void OnNPCAtStockpile (ref NPCBase.NPCState state)
		{
			state.Inventory.Dump(usedNPC.Colony.UsedStockpile);
			state.SetCooldown(0.1);
			state.JobIsDone = true;
		}

		public virtual void OnRemove ()
		{
			isValid = false;
			NPC = null;
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
			if (!worldTypeChecked) { CheckWorldType(); }
			return KeyLocation;
		}

		public virtual ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			return this;
		}

		protected virtual void OnChangedGoal (NPCBase.NPCGoal oldGoal, NPCBase.NPCGoal newGoal)
		{

		}

		protected virtual void CheckWorldType ()
		{
			if (World.TryGetTypeAt(position, out worldType)) {
				worldTypeChecked = true;
				if (!IsValidWorldType(worldType)) {
					Log.WriteWarning("Removing job at {0} because the world type found was not allowed ({1}), job type {2}", position, ItemTypes.IndexLookup.GetName(worldType), GetType());
					BlockJobManagerTracker.RemoveBlockTypeAt(GetType(), position);
				}
			}
		}

		protected virtual bool IsValidWorldType (ushort type)
		{
			return type != 0;
		}
	}
}
