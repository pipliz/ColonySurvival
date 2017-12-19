using Pipliz.JSON;
using Server.GrowableBlocks;

namespace Pipliz.Mods.APIProvider.GrowableBlocks
{
	public class GrowableBlock<T> : IGrowableBlock where T : IGrowableBlockDefinition
	{
		protected double lastUpdateHours;
		protected Vector3Int position;
		protected float growth;
		protected byte currentStageIndex;
		protected bool isValid = true;

		protected static T TypeSingleton;

		public virtual byte CurrentStageIndex
		{
			get { return currentStageIndex; }
			set { currentStageIndex = value; }
		}

		/// <summary>
		/// Time spent growing in ingame hours
		/// </summary>
		public virtual float Growth
		{
			get { return growth; }
			set { growth = value; }
		}

		/// <summary>
		/// Last time this was updated, total ingame hours
		/// </summary>
		public virtual double LastUpdateHours
		{
			get { return lastUpdateHours; }
			set { lastUpdateHours = value; }
		}

		public virtual bool IsValid { get { return isValid; } }

		/// <summary>
		/// Returns the IGrowableBlockDefinition instance managing this type of block. Should return a single instance for all IGrowableBlocks of the same type
		/// </summary>
		public virtual IGrowableBlockDefinition Definition
		{
			get
			{
				if (TypeSingleton == null) {
					GrowableBlockManager.TryGetGrowableBlockDefinitionInstance(out TypeSingleton);
				}
				return TypeSingleton;
			}
		}

		public virtual Vector3Int Position { get { return position; } }

		public GrowableBlock (Vector3Int position, float growth = 0f, byte currentStageIndex = 0)
		{
			this.position = position;
			this.growth = growth;
			this.currentStageIndex = currentStageIndex;
		}

		public virtual void OnStopTracking ()
		{
			isValid = false;
		}

		public virtual void SetInvalid ()
		{
			isValid = false;
		}

		public virtual JSONNode GetJSON ()
		{
			double timeNow = TimeCycle.TotalTime;
			double extraGrowth = timeNow - lastUpdateHours;
			if (extraGrowth > 0.0) {
				growth += (float)extraGrowth;
			}
			lastUpdateHours = timeNow;

			return new JSONNode()
				.SetAs("x", position.x)
				.SetAs("y", position.y)
				.SetAs("z", position.z)
				.SetAs("s", (int)currentStageIndex)
				.SetAs("g", growth);
		}
	}
}
