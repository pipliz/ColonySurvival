using NPC;
using System.Linq;

namespace Pipliz.Mods.APIProvider.AreaJobs
{
	using Areas;
	using JSON;
	using static NPC.NPCBase;

	public class DefaultFarmerAreaJob<T> : IJob, IAreaJob where T : IAreaJobDefinition
	{
		protected Vector3Int positionSub = Vector3Int.invalidPos;
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;
		protected bool shouldDumpInventory = false;

		public virtual Colony Owner { get; protected set; }
		public virtual bool IsValid { get; protected set; }
		public virtual NPCBase NPC { get; protected set; }

		protected static T TypeSingleton;

		public virtual Vector3Int KeyLocation { get { return positionMin; } }
		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual bool NeedsNPC { get { return NPC == null || !NPC.IsValid; } }
		public virtual InventoryItem RecruitmentItem { get { return InventoryItem.Empty; } }
		public virtual NPCType NPCType { get { return Definition.UsedNPCType; } }
		public virtual bool NeedsItems { get { return shouldDumpInventory; } }
		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }
		public virtual Shared.EAreaType AreaType { get { return Definition.AreaType; } }
		public virtual Shared.EAreaMeshType AreaTypeMesh { get { return Shared.EAreaMeshType.AutoSelect; } }

		public virtual IAreaJobDefinition Definition
		{
			get
			{
				if (TypeSingleton == null) {
					TypeSingleton = AreaJobTracker.RegisteredAreaJobDefinitions
						.Where(instance => instance.GetType() == typeof(T))
						.Select(instance => (T)instance)
						.First();
				}
				return TypeSingleton;
			}
		}


		public DefaultFarmerAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			IsValid = true;
			positionMin = min;
			positionMax = max;
			Owner = owner;

			NPCBase foundNPC = null;
			if (npcID != 0) {
				NPCTracker.TryGetNPC(npcID, out foundNPC);
			}
			SetNPC(foundNPC, true);
		}

		public virtual NPCGoal CalculateGoal (ref NPCState state)
		{
			if (ToSleep) {
				return state.Inventory.IsEmpty ? NPCGoal.Bed : NPCGoal.Stockpile;
			} else {
				return state.Inventory.Full || NeedsItems ? NPCGoal.Stockpile : NPCGoal.Job;
			}
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

		public virtual void OnNPCAtJob (ref NPCState state)
		{
			Definition.OnNPCAtJob(this, ref positionSub, ref state, ref shouldDumpInventory);
		}

		public virtual void OnNPCAtStockpile (ref NPCState state)
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
			IsValid = false;
			NPC = null;
			Owner.JobFinder.Remove(this);
		}

		public virtual void SaveAreaJob ()
		{
			Definition.SaveJob(Owner, new JSONNode()
				.SetAs("npcID", NPC == null ? 0 : NPC.ID)
				.SetAs("min", (JSONNode)positionMin)
				.SetAs("max", (JSONNode)positionMax)
			);
		}

		public virtual void TakeItems (ref NPCState state)
		{
			if (TryDumpNPCInventory(ref state)) {
				state.JobIsDone = true;
			}
			shouldDumpInventory = false;
		}

		protected bool TryDumpNPCInventory (ref NPCState npcState)
		{
			npcState.Inventory.Dump(NPC.Colony.Stockpile);
			return true;
		}

		public void SetNPC (NPCBase newNPC)
		{
			SetNPC(newNPC, false);
		}

		public void SetNPC (NPCBase newNPC, bool callTakeJob)
		{
			if (NPC != newNPC) {
				NPC?.ClearJob();
				NPC = newNPC;
				if (NPC == null) {
					Owner.JobFinder.Add(this);
				} else {
					if (callTakeJob) {
						NPC.TakeJob(this);
					}
				}
			} else if (newNPC == null) {
				Owner.JobFinder.Add(this);
			}
		}
	}
}
