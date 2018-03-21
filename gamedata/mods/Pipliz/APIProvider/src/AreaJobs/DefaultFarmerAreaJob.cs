using NPC;
using Server.NPCs;

namespace Pipliz.Mods.APIProvider.AreaJobs
{
	using JSON;

	public class DefaultFarmerAreaJob<T> : IJob, IAreaJob where T : IAreaJobDefinition
	{
		protected NPCBase usedNPC;
		protected Vector3Int positionSub = Vector3Int.invalidPos;
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;
		protected bool shouldDumpInventory = false;
		protected Players.Player owner;
		protected bool isValid = true;

		protected static T TypeSingleton;

		public virtual Vector3Int KeyLocation { get { return positionMin; } }
		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual Players.Player Owner { get { return owner; } }
		public virtual bool IsValid { get { return isValid; } }
		public virtual bool NeedsNPC { get { return usedNPC == null || !usedNPC.IsValid; } }
		public virtual InventoryItem RecruitementItem { get { return InventoryItem.Empty; } }
		public virtual NPCType NPCType { get { return Definition.UsedNPCType; } }
		public virtual bool NeedsItems { get { return shouldDumpInventory; } }
		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }
		public virtual Shared.EAreaType AreaType { get { return Definition.AreaType; } }
		public virtual Shared.EAreaMeshType AreaTypeMesh { get { return Shared.EAreaMeshType.AutoSelect; } }

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

		public virtual IAreaJobDefinition Definition
		{
			get
			{
				if (TypeSingleton == null) {
					foreach (var instance in AreaJobTracker.RegisteredAreaJobDefinitions) {
						if (instance.GetType() == typeof(T)) {
							TypeSingleton = (T)instance;
						}
					}
				}
				return TypeSingleton;
			}
		}


		public DefaultFarmerAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			positionMin = min;
			positionMax = max;
			this.owner = owner;

			NPCBase foundNPC = null;
			if (npcID != 0) {
				NPCTracker.TryGetNPC(npcID, out foundNPC);
			}
			NPC = foundNPC;
		}

		public virtual NPCBase.NPCGoal CalculateGoal (ref NPCBase.NPCState state)
		{
			if (ToSleep) {
				if (!state.Inventory.IsEmpty) {
					return NPCBase.NPCGoal.Stockpile;
				}
				return NPCBase.NPCGoal.Bed;
			}
			if (state.Inventory.Full || NeedsItems) {
				return NPCBase.NPCGoal.Stockpile;
			}
			return NPCBase.NPCGoal.Job;
		}

		public virtual void CalculateSubPosition ()
		{
			Definition.CalculateSubPosition(this, ref positionSub);
		}

		public virtual Vector3Int GetJobLocation ()
		{
			if (!positionSub.IsValid) {
				CalculateSubPosition();
			}
			return positionSub;
		}

		public virtual void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			Definition.OnNPCAtJob(this, ref positionSub, ref state, ref shouldDumpInventory);
		}

		public virtual void OnNPCAtStockpile (ref NPCBase.NPCState state)
		{
			if (ToSleep) {
				TryDumpNPCInventory(ref state);
				state.JobIsDone = true;
				state.SetCooldown(0.3);
			} else {
				TakeItems(ref state);
				state.SetCooldown(0.3);
			}
		}

		public virtual void OnRemove ()
		{
			Definition.OnRemove(this);
			isValid = false;
			NPC = null;
			JobTracker.Remove(owner, KeyLocation);
		}

		public virtual void SaveAreaJob ()
		{
			Definition.SaveJob(owner, new JSONNode()
				.SetAs("npcID", usedNPC == null ? 0 : usedNPC.ID)
				.SetAs("min", (JSONNode)positionMin)
				.SetAs("max", (JSONNode)positionMax)
			);
		}

		public virtual void TakeItems (ref NPCBase.NPCState state)
		{
			if (TryDumpNPCInventory(ref state)) {
				state.JobIsDone = true;
			}
			shouldDumpInventory = false;
		}

		protected bool TryDumpNPCInventory (ref NPCBase.NPCState npcState)
		{
			npcState.Inventory.Dump(usedNPC.Colony.UsedStockpile);
			return true;
		}
	}
}
