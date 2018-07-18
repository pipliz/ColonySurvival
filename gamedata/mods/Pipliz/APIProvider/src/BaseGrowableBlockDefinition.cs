using System.Collections.Generic;
using BlockEntities;
using System.Linq;

namespace Pipliz.Mods.APIProvider.GrowableBlocks
{
	using UnityEngine.Assertions;

	// ILoadedByPositionType is only used as a backup if the data one has problems
	public abstract class BaseGrowableBlockDefinition : ILoadedWithDataByPositionType, ILoadedByPositionType, IChangedWithType, IUnloadedByPosition, IMultiBlockEntityAutoLoader
	{
		public virtual GrowableStage[] Stages { get; protected set; }
		public virtual ItemTypes.ItemType[] StageTypes { get; protected set; }
		public virtual EGrowthType GrowthType { get; protected set; }
		public virtual float RandomStartGrowthMax { get; protected set; }

		public virtual IEnumerable<ItemTypes.ItemType> TypesToRegister { get { return StageTypes; } }

		public virtual void SetStages (IEnumerable<GrowableStage> stages)
		{
			Stages = stages.ToArray();
			StageTypes = stages.Select(stage => stage.BlockType).ToArray();
			Assert.IsTrue(Stages.Length < byte.MaxValue - 1, "Stages too long! Supports max byte.MaxValue - 1 indices");
		}

		// IChangedWithType
		public virtual void OnChangedWithType (Players.Player player, Vector3Int blockPosition, ItemTypes.ItemType typeOld, ItemTypes.ItemType typeNew)
		{
			bool isRemoved = StageTypes.ContainsByReference(typeOld);
			bool isAdded = StageTypes.ContainsByReference(typeNew);
			if (isAdded) {
				if (isRemoved) {
					// updated type (grew some_stage -> some_stage)
				} else {
					OnPlaced(blockPosition, typeNew, null);
				}
			} else if (isRemoved) {
				OnRemoved(blockPosition);
			}
		}

		// IUnloadedByPosition
		public virtual void OnUnloadedPosition (Vector3Int blockPosition)
		{
			OnRemoved(blockPosition);
		}

		// ILoadedByPositionType
		public virtual void OnLoadedPositionType (Vector3Int blockPosition, ushort type)
		{
#if DEBUG
			Log.WriteWarning("Loaded block at {0} without data, expected data!", blockPosition);
#endif
			OnPlaced(blockPosition, ItemTypes.GetType(type), null);
		}

		// ILoadedWithDataByPositionType
		public virtual void OnLoadedWithDataPosition (Vector3Int blockPosition, ushort type, ByteReader reader)
		{
			OnPlaced(blockPosition, ItemTypes.GetType(type), reader);
		}

		// called by loadwithdata/load/changed
		public virtual void OnPlaced (Vector3Int blockPosition, ItemTypes.ItemType typeNew, ByteReader reader)
		{
			GrowableBlock block = new GrowableBlock(this, typeNew, reader);
			if (block.IsValid) {
				ServerManager.BlockEntityTracker.OnAddedEntity(blockPosition, block);
			}
		}

		// called by unload/changed
		public virtual void OnRemoved (Vector3Int blockPosition)
		{
			ServerManager.BlockEntityTracker.OnRemoveEntity(blockPosition);
		}

		public virtual void TryAdvanceStage (GrowableBlock block, Chunk chunk, Vector3Int blockPosition)
		{
			if (chunk == null || chunk.DataState != Chunk.ChunkDataState.DataFull) {
				return;
			}

			byte nextStageIndex = (byte)(block.StageIndex + 1);

			if (nextStageIndex >= Stages.Length) {
				block.SetInvalid();
				return;
			}
			// more stages to go
			GrowableStage nextStage = Stages[nextStageIndex];
			ushort oldType;
			if (!chunk.TryGetTypeAt(blockPosition.ToChunkLocal(), out oldType)) {
				// can't read type for some reason
				return;
			}

			if (oldType == 0) {
				// no block... certainly not a valid stage
				block.SetInvalid();
				return;
			}
			if (oldType != Stages[block.StageIndex].BlockType.ItemIndex) {
				// ?? current block type does not match current stage type.
				// Try to recover what stage to be

				for (int i = 0; i < Stages.Length; i++) {
					if (Stages[i].BlockType.ItemIndex == oldType) {
						block.StageIndex = (byte)i;
						return;
					}
				}
				block.SetInvalid();
				return;
			}

			// don't trigger entity callbacks on upgrading the type - not needed (and they'll deadlock)
			if (ServerManager.TryChangeBlock(blockPosition, nextStage.BlockType.ItemIndex, flags: ServerManager.SetBlockFlags.Default & ~ServerManager.SetBlockFlags.TriggerEntityCallbacks)) {
				if (nextStageIndex == Stages.Length - 1) {
					// reached last stage
					block.SetInvalid();
				} else {
					block.StageIndex = nextStageIndex;
					switch (GrowthType) {
						case EGrowthType.Always:
						default:
							block.Growth = Random.NextFloat(0f, RandomStartGrowthMax * nextStage.GrowthTime);
							break;
						case EGrowthType.FirstNightRandom:
							block.Growth = GetRandomGrowthTillNight();
							break;

					}
				}
			}
		}

		public class GrowableBlock : IBlockEntityUpdatable, IBlockEntitySerializable
		{
			public virtual BaseGrowableBlockDefinition BlockDefinition { get; set; }
			public virtual byte StageIndex { get; set; }
			public virtual float Growth { get; set; }
			public virtual double LastUpdateTimeCycleHours { get; set; }

			public virtual bool IsValid { get { return BlockDefinition != null; } }

			public GrowableBlock (BaseGrowableBlockDefinition blockDef, ItemTypes.ItemType type, ByteReader data)
			{
				BlockDefinition = blockDef;
				bool foundStage = false;
				var stages = BlockDefinition.StageTypes;
				for (int i = 0; i < stages.Length; i++) {
					if (stages[i] == type) {
						StageIndex = (byte)i;
						foundStage = true;
					}
				}
				if (!foundStage) {
					// marks block as invalid
					BlockDefinition = null;
					return;
				}

				if (data != null) {
					Growth = data.ReadF32();
				} else {
					switch (BlockDefinition.GrowthType) {
						case EGrowthType.Always:
							Growth = Random.NextFloat(0f, BlockDefinition.RandomStartGrowthMax * BlockDefinition.Stages[StageIndex].GrowthTime);
							break;
						case EGrowthType.FirstNightRandom:
							Growth = GetRandomGrowthTillNight();
							break;
						default:
							Growth = 0f;
							break;
					}
				}
				LastUpdateTimeCycleHours = TimeCycle.TotalTime;
			}

			public virtual void SetInvalid ()
			{
				BlockDefinition = null;
			}

			public virtual double GetFirstUpdate ()
			{
				return Time.SecondsSinceStartDoubleThisFrame + 0.5;
			}

			public virtual EUpdatableBlockEntityResult OnEntityUpdate (Chunk chunk, Vector3Int blockPosition, double timeUpdate, out double nextUpdate)
			{
				nextUpdate = 0.0;
				double TimeCycleHours = TimeCycle.TotalTime;
				double timeDif = TimeCycleHours - LastUpdateTimeCycleHours;
				LastUpdateTimeCycleHours = TimeCycleHours;
				if (timeDif < 0.0) {
					timeDif = 0.0;
				}
				chunk.IsDirty = true;

				switch (BlockDefinition.GrowthType) {
					case EGrowthType.FirstNightRandom: {
							// growth is the time in ingame hours till the next night growth happens
							Growth -= (float)timeDif;
							if (Growth <= 0f) {
								BlockDefinition.TryAdvanceStage(this, chunk, blockPosition);
							}
							break;
						}
					case EGrowthType.Always: {
							// growth is accumulating till it hits the stage max
							Growth += (float)timeDif;
							if (Growth >= BlockDefinition.Stages[StageIndex].GrowthTime) {
								BlockDefinition.TryAdvanceStage(this, chunk, blockPosition);
							}
							break;
						}
					default:
						Log.WriteError("Unexpected growthType: {0}", BlockDefinition.GrowthType);
						return EUpdatableBlockEntityResult.StopTracking;
				}

				if (IsValid) {
					nextUpdate = Time.SecondsSinceStartDoubleThisFrame + Random.NextFloat(20f, 40f);
					return EUpdatableBlockEntityResult.KeepUpdating;
				} else {
					return EUpdatableBlockEntityResult.StopTracking;
				}
			}

			public virtual void SerializeToBytes (Vector3Int blockPosition, ByteBuilder builder)
			{
				builder.Write(Growth);
			}
		}

		public static float GetRandomGrowthTillNight ()
		{
			float timeToSunSet = TimeCycle.TimeTillSunSet;
			float timeToSunRise = TimeCycle.TimeTillSunRise;
			if (timeToSunSet > timeToSunRise) {
				// say sunset in 15 hours, sunrise in 6 hours
				// so it's currently night
				// add 24h to sunrise, so it's the sunrise after the sunset
				timeToSunRise += 24f;
			} else {
				// say sunset in 6 houts, sunrise in 15 hours
				// it's the next night.
			}
			return Random.NextFloat(timeToSunSet + 0.2f, timeToSunRise - 0.2f);
		}

		public enum EGrowthType
		{
			FirstNightRandom,
			Always
		}

		public struct GrowableStage
		{
			public ItemTypes.ItemType BlockType { get; private set; }
			public float GrowthTime { get; private set; }

			public GrowableStage (string blockName, float growthTime = 10f) : this ()
			{
				BlockType = ItemTypes.GetType(blockName);
				GrowthTime = growthTime;
			}
		}
	}
}
