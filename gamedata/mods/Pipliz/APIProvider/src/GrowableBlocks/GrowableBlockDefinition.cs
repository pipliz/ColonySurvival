using Pipliz.Helpers;
using Server.GrowableBlocks;
using System.Collections.Generic;
using System.Threading;

namespace Pipliz.Mods.APIProvider.GrowableBlocks
{
	using JSON;

	/// <summary>
	/// Builtin implementation of the IGrowableBlockDefinition class, generic/overridable enough for builtin use.
	/// </summary>
	/// <typeparam name="T">The type that inheritted this</typeparam>
	public class GrowableBlockDefinition<T> : IGrowableBlockDefinition where T : IGrowableBlockDefinition
	{
		protected List<IGrowableStage> Stages;
		protected string FileName;
		protected EGrowthType GrowthType;

		
		protected float RandomStartGrowthMax = 0f; // Percentage done growing a stage can start at

		protected JSONNode SaveArray; // used temporarily while saving
		protected int BlockCountToSave; // used in preparesaving to track the capacity of the array to create

		protected JSONNode LoadedRoot; // used temporarily while loading
		protected ManualResetEvent FinishedLoadingEvent = new ManualResetEvent(false);

		public virtual IList<IGrowableStage> GrowableStages { get { return Stages; } }

		public virtual string FilePath { get { return string.Format("gamedata/savegames/{0}/growables/{1}.json", ServerManager.WorldName, FileName); } }

		/// <summary>
		/// Tracking new block placed in the world
		/// </summary>
		public virtual IGrowableBlock MakeGrowableBlock (Vector3Int position, ushort type, Players.Player player)
		{
			return new GrowableBlock<T>(position, Random.NextFloat(0f, RandomStartGrowthMax * Stages[0].GrowthTime));
		}

		/// <summary>
		/// Loading a block from JSON
		/// </summary>
		public virtual IGrowableBlock MakeGrowableBlock (JSONNode node)
		{
			Vector3Int position;
			position.x = node.GetAsOrDefault("x", -1);
			position.y = node.GetAsOrDefault("y", -1);
			position.z = node.GetAsOrDefault("z", -1);
			float growth = node.GetAsOrDefault("g", 0f);
			byte stage = node.GetAsOrDefault("s", (byte)0);
			return new GrowableBlock<T>(position, growth, stage);
		}

		/// <summary>
		/// Loading a block from a legacy JSON node format
		/// </summary>
		public virtual IGrowableBlock MakeGrowableBlockLegacy (JSONNode node)
		{
			Vector3Int position = Vector3Int.invalidPos;
			JSONNode locationNode;
			if (node.TryGetChild("location", out locationNode)) {
				position.x = locationNode.GetAsOrDefault("x", -1);
				position.y = locationNode.GetAsOrDefault("y", -1);
				position.z = locationNode.GetAsOrDefault("z", -1);
			}
			float growth = node.GetAsOrDefault("growth", 0f);
			return new GrowableBlock<T>(position, growth, 0);
		}

		/// <summary>
		/// Reports which blocks are tracked in a first iteration to prepare for saving.
		/// Track how many blocks there are to initialize the array at proper capacity later
		/// </summary>
		public virtual void PrepareSaving (IGrowableBlock block)
		{
			BlockCountToSave++;
		}

		/// <summary>
		/// Called to save this specific block
		/// </summary>
		public virtual void SaveBlock (IGrowableBlock block)
		{
			if (SaveArray == null) {
				SaveArray = new JSONNode(NodeType.Array);
				SaveArray.SetArrayCapacity(BlockCountToSave);
				BlockCountToSave = 0; // reset capacity for next autosave/quit
			}
			SaveArray.AddToArray(block.GetJSON());
		}

		/// <summary>
		/// Called after all blocks are processed by SaveBlock
		/// </summary>
		public virtual void FinishSaving ()
		{
			string filePath = FilePath;
			JSONNode arrayCopy = SaveArray;
			// use StartAsyncQuitToComplete so that the game does not wait on writing to disk
			// it'll wait for this to complete before quitting
			// runs in the thread pool
			General.Application.StartAsyncQuitToComplete(delegate ()
			{
				JSONNode root = new JSONNode();
				root.SetAs("version", 0);
				if (arrayCopy == null) {
					arrayCopy = new JSONNode(NodeType.Array);
				}
				root.SetAs("array", arrayCopy);
				IOHelper.CreateDirectoryFromFile(filePath);
				JSON.Serialize(filePath, root);
			});

			SaveArray = null;
		}

		/// <summary>
		/// Called for every definition to start loading
		/// </summary>
		public virtual void StartLoading ()
		{
			// load async to prevent waiting on disk...
			ThreadPool.QueueUserWorkItem(AsyncLoad);
		}

		protected virtual void AsyncLoad (object obj)
		{
			try {
				JSON.Deserialize(FilePath, out LoadedRoot, false);
			} finally {
				FinishedLoadingEvent.Set();
			}
		}

		/// <summary>
		/// Called for every definition after StartLoading has been called on every one of them
		/// Waits for loading to finish and calls LoadJSON
		/// </summary>
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

		/// <summary>
		/// Called with the root json node to load the data from
		/// </summary>
		public virtual void LoadJSON (JSONNode node)
		{
			JSONNode array = node.GetAs<JSONNode>("array");
			for (int i = 0; i < array.ChildCount; i++) {
				GrowableBlockManager.RegisterGrowableBlock(MakeGrowableBlock(array[i]));
			}
		}

		/// <summary>
		/// Called by GrowableBlockManager to process an update to a block
		/// Also called very soon after block creation
		/// </summary>
		/// <param name="timeNextUpdateHours">The time in timecycle hours when to call UpdateBlock again</param>
		/// <returns>True if there should be another UpdateBlock call at 'timeNextUpdateHours'</returns>
		public virtual bool UpdateBlock (IGrowableBlock block, double timeNowHours, out double timeNextUpdateHours)
		{
			timeNextUpdateHours = 0.0;
			if (block == null) {
				return false; // discard null blocks
			}
			if (block.CurrentStageIndex >= Stages.Count) {
				return false; // completed growth, discard
			}
			if (!block.IsValid) {
				return false;
			}

			switch (GrowthType) {
				case EGrowthType.FirstNightRandom:
					return DoUpdateFirstNightRandom(block, timeNowHours, out timeNextUpdateHours);
				case EGrowthType.Always:
					return DoUpdateAlways(block, timeNowHours, out timeNextUpdateHours);
				default:
					Log.WriteError("Unexpected growthType: {0}", GrowthType);
					return false;
			}
		}

		/// <summary>
		/// Process update from a block that grows 24/7 (saplings)
		/// </summary>
		protected virtual bool DoUpdateAlways (IGrowableBlock block, double timeNowHours, out double timeNextUpdateHours)
		{
			timeNextUpdateHours = 0.0;
			double timeDif = timeNowHours - block.LastUpdateHours;

			if (timeDif > 0.0) {
				block.Growth = block.Growth + (float)timeDif;
			}

			block.LastUpdateHours = timeNowHours;

			float growthLeft = Stages[block.CurrentStageIndex].GrowthTime - block.Growth;
			if (growthLeft <= 0f) {
				if (TryAdvanceStage(block, block.CurrentStageIndex)) {
					if (!block.IsValid) {
						return false;
					}
					growthLeft = Stages[block.CurrentStageIndex].GrowthTime - block.Growth;
					timeNextUpdateHours = timeNowHours + growthLeft;
					return true;
				} else {
					// area not loaded, check again in 0.5 - 1.5 ingame hours
					timeNextUpdateHours = timeNowHours + Random.NextDouble(0.5, 1.5);
					return true;
				}
			}

			// growth not completed
			timeNextUpdateHours = timeNowHours + growthLeft + 0.1f;
			return true;
		}

		/// <summary>
		/// Process update from a block that grows every night at a random time (wheat, flax)
		/// </summary>
		protected virtual bool DoUpdateFirstNightRandom (IGrowableBlock block, double timeNowHours, out double timeNextUpdateHours)
		{
			timeNextUpdateHours = 0.0;
			double timeDif = timeNowHours - block.LastUpdateHours;

			if (timeDif > 0.0) {
				block.Growth = block.Growth + (float)timeDif;
			}

			block.LastUpdateHours = timeNowHours;

			float growthLeft = Stages[block.CurrentStageIndex].GrowthTime - block.Growth;
			if (growthLeft <= 0f) {
				if (TryAdvanceStage(block, block.CurrentStageIndex)) {
					if (!block.IsValid) {
						return false;
					}
					// block grew, update again randomly in next night
					timeNextUpdateHours = timeNowHours + TimeCycle.TimeTillSunSet + Random.NextDouble(0.5, TimeCycle.NightLength - 0.5);
					return true;
				} else {
					// area not loaded, check again in 0.5 - 1.5 ingame hours
					timeNextUpdateHours = timeNowHours + Random.NextDouble(0.5, 1.5);
					return true;
				}
			}

			// growth not completed
			float timeTillSunRise = TimeCycle.TimeTillSunRise;
			if (growthLeft <= timeTillSunRise) {
				// can grow this night
				float nightLength = TimeCycle.NightLength;
				if (timeTillSunRise < nightLength) {
					// night already started
					timeNextUpdateHours = timeNowHours + Random.NextDouble(growthLeft, timeTillSunRise);
				} else {
					// night not started yet
					timeNextUpdateHours = timeNowHours + Random.NextDouble(timeTillSunRise - nightLength + 0.5f, timeTillSunRise - 0.5);
				}
			} else {
				// can't grow this/coming night, wait a day ish
				timeNextUpdateHours = timeNowHours + Random.NextDouble (18.0, 24.0);
			}
			return true;
		}

		/// <summary>
		/// Called when a stage is grown.
		/// By default places the BlockType of the stage at the position
		/// </summary>
		public virtual bool TryAdvanceStage (IGrowableBlock block, byte currentStageIndex)
		{
			byte nextStageIndex = (byte)(currentStageIndex + 1);

			if (nextStageIndex < Stages.Count) {
				// more stages to go
				IGrowableStage nextStage = Stages[nextStageIndex];
				ushort oldType;
				if (World.TryGetTypeAt(block.Position, out oldType)) {
					if (oldType == 0) {
						// no block... certainly not a valid stage
						block.SetInvalid();
						return true;
					}
					if (oldType != Stages[currentStageIndex].BlockTypeIndex) {
						// ?? current block type does not match current stage type.
						// Probably legacy imported blocks
						// Try to recover what stage to be

						for (int i = 0; i < Stages.Count; i++) {
							if (Stages[i].BlockTypeIndex == oldType) {
								block.CurrentStageIndex = (byte)i;
								return false;
							}
						}
						block.SetInvalid();
						return true;
					}
				} else {
					return false;
				}

				if (ServerManager.TryChangeBlock (block.Position, nextStage.BlockTypeIndex)) {
					if (nextStageIndex == Stages.Count - 1) {
						// reached last stage
						block.SetInvalid();
					} else {
						block.CurrentStageIndex = nextStageIndex;
						block.Growth = Random.NextFloat(0f, RandomStartGrowthMax * Stages[nextStageIndex].GrowthTime);
					}
					return true;
				} else {
					return false;
				}
			} else {
				// at last stage already
				block.SetInvalid();
				return true;
			}
		}

		protected enum EGrowthType
		{
			FirstNightRandom,
			Always
		}
	}
}
