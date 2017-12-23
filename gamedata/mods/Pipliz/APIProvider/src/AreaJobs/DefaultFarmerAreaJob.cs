using NPC;
using Server.NPCs;

namespace Pipliz.Mods.APIProvider.AreaJobs
{
	using JSON;

	public class DefaultFarmerAreaJob<T> : Job, IAreaJob where T : IAreaJobDefinition
	{
		protected Vector3Int positionSub = Vector3Int.invalidPos;
		protected Vector3Int positionMin;
		protected Vector3Int positionMax;
		protected bool shouldDumpInventory = false;

		protected static T TypeSingleton;

		public virtual Vector3Int Minimum { get { return positionMin; } }
		public virtual Vector3Int Maximum { get { return positionMax; } }
		public virtual NPCBase UsedNPC { get { return usedNPC; } }

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

		public override NPCType NPCType { get { return Definition.UsedNPCType; } }

		public override bool NeedsItems { get { return shouldDumpInventory; } }

		public DefaultFarmerAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			positionMin = min;
			positionMax = max;
			InitializeJob(owner, min, npcID);
		}

		public virtual void CalculateSubPosition ()
		{
			Definition.CalculateSubPosition(this, ref positionSub);
		}

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			Definition.OnNPCAtJob(this, ref positionSub, ref state, ref shouldDumpInventory);
		}

		public override void TakeItems (ref NPCBase.NPCState state)
		{
			base.TakeItems(ref state);
			shouldDumpInventory = false;
		}

		public override Vector3Int GetJobLocation ()
		{
			if (!positionSub.IsValid) {
				CalculateSubPosition();
			}
			return positionSub;
		}

		public override JSONNode GetJSON ()
		{
			return base.GetJSON()
				.SetAs("min", (JSONNode)positionMin)
				.SetAs("max", (JSONNode)positionMax);
		}

		public virtual void SaveAreaJob ()
		{
			Definition.SaveJob(owner, GetJSON());
		}

		public override void OnRemove ()
		{
			Definition.OnRemove(this);
			base.OnRemove();
		}

		protected void SetLayer (ushort type, int layer)
		{
			int yLayer = positionMin.y + layer;
			for (int x = positionMin.x; x <= positionMax.x; x++) {
				for (int z = positionMin.z; z <= positionMax.z; z++) {
					ServerManager.TryChangeBlock(new Vector3Int(x, yLayer, z), type);
				}
			}
		}
	}
}
