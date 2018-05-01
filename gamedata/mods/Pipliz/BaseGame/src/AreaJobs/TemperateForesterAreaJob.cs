using BlockTypes.Builtin;
using NPC;
using System.IO;
using System.Threading;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using JSON;

	[AreaJobDefinitionAutoLoader]
	public class TemperateForesterDefinition : AreaJobDefinitionDefault<TemperateForesterDefinition>
	{
		public TemperateForesterDefinition ()
		{
			identifier = "pipliz.temperateforest";
			fileName = "temperateforester";
			npcType = Server.NPCs.NPCType.GetByKeyNameOrDefault("pipliz.forester");
			areaType = Shared.EAreaType.Forestry;
		}

		public override IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			SetLayer(min, max, BuiltinBlocks.LumberArea, -1, owner);
			return base.CreateAreaJob(owner, min, max, npcID);
		}

		public override void OnRemove (IAreaJob job)
		{
			SetLayer(job.Minimum, job.Maximum, BuiltinBlocks.GrassTemperate, -1, job.Owner);
		}

		public override void CalculateSubPosition (IAreaJob job, ref Vector3Int positionSub)
		{
			bool hasSeeds = job.NPC.Colony.UsedStockpile.Contains(BuiltinBlocks.Sapling);
			Vector3Int firstPlanting = Vector3Int.invalidPos;
			Vector3Int min = job.Minimum;
			Vector3Int max = job.Maximum;

			for (int x = min.x + 1; x < max.x; x += 3) {
				for (int z = min.z + 1; z < max.z; z += 3) {
					ushort type;
					Vector3Int possiblePositionSub = new Vector3Int(x, min.y, z);
					if (!World.TryGetTypeAt(possiblePositionSub, out type)) {
						positionSub = min;
						return;
					}
					if (type == 0) {
						if (hasSeeds) {
							positionSub = possiblePositionSub; // can plant here
							return;
						} else if (!firstPlanting.IsValid) {
							firstPlanting = possiblePositionSub; // no seeds but should plant here. If no location found, go here
						}
					} else if (type == BuiltinBlocks.LogTemperate) {
						positionSub = possiblePositionSub; // should cut here
						return;
					}
				}
			}

			if (firstPlanting.IsValid) {
				positionSub = firstPlanting;
				return;
			}

			int xOffset = max.x - min.x;
			int zOffset = max.z - min.z;
			int xRandom = Random.Next(0, xOffset / 3) * 3 + min.x;
			int zRandom = Random.Next(0, zOffset / 3) * 3 + min.z;
			positionSub = new Vector3Int(xRandom, min.y, zRandom);
		}

		static System.Collections.Generic.List<ItemTypes.ItemTypeDrops> GatherResults = new System.Collections.Generic.List<ItemTypes.ItemTypeDrops>();

		public override void OnNPCAtJob (IAreaJob job, ref Vector3Int positionSub, ref NPCBase.NPCState state, ref bool shouldDumpInventory)
		{
			state.JobIsDone = true;
			Vector3Int min = job.Minimum;
			Vector3Int max = job.Maximum;
			if (positionSub.x == min.x || positionSub.x == max.x
				|| positionSub.z == min.z || positionSub.z == max.z
				|| (positionSub.x - (min.x + 1)) % 3 != 0
				|| (positionSub.z - (min.z + 1)) % 3 != 0)
			{
				ushort type;
				if (World.TryGetTypeAt(positionSub.Add(1, 0, 1), out type)) {
					if (type == BuiltinBlocks.Sapling) {
						Server.GrowableBlocks.IGrowableBlock block;
						if (Server.GrowableBlocks.GrowableBlockManager.TryGetGrowableBlock(positionSub.Add(1, 0, 1), out block)) {
							state.SetCooldown(5.0);
						} else {
							ItemTypesServer.OnChange(positionSub.Add(1, 0, 1), 0, BuiltinBlocks.Sapling, null);
							state.SetIndicator(new Shared.IndicatorState(2f, BuiltinBlocks.Sapling));
							state.SetCooldown(0.2);
						}
					} else {
						state.SetCooldown(1.0); // no sapling at sapling spot (shouldn't occur a lot, something changed between calculate sub position and this
					}
				} else {
					state.SetCooldown(4.0); // walked to sapling spot, not loaded
				}
			} else if (positionSub.IsValid) {
				ushort type;
				if (World.TryGetTypeAt(positionSub, out type)) {
					if (type == 0) {
						if (job.NPC.Inventory.TryGetOneItem(BuiltinBlocks.Sapling)
							|| job.NPC.Colony.UsedStockpile.TryRemove(BuiltinBlocks.Sapling)) {
							ServerManager.TryChangeBlock(positionSub, BuiltinBlocks.Sapling, job.Owner, ServerManager.SetBlockFlags.DefaultAudio);
							state.SetCooldown(2.0);
						} else {
							state.SetIndicator(new Shared.IndicatorState(2f, BuiltinBlocks.Sapling));
						}
					} else if (type == BuiltinBlocks.LogTemperate) {
						if (ChopTree(positionSub, job.Owner)) {
							state.SetIndicator(new Shared.IndicatorState(10f, BuiltinBlocks.LogTemperate));
							ServerManager.SendAudio(positionSub.Vector, "woodDeleteHeavy");

							GatherResults.Clear();
							GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.LogTemperate, 3, 1.0));
							GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.LeavesTemperate, 9, 1.0));
							GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.Sapling, 1, 1.0));
							GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.Sapling, 1, 0.25));

							ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, job as IJob, positionSub, GatherResults);

							job.NPC.Inventory.Add(GatherResults);
						} else {
							state.SetCooldown(Random.NextFloat(3f, 6f));
						}
					} else {
						state.SetCooldown(Random.NextFloat(8f, 16f));
					}
				} else {
					state.SetCooldown(Random.NextFloat(3f, 6f));
				}
			} else {
				state.SetCooldown(10.0);
			}
			positionSub = Vector3Int.invalidPos;
		}

		static bool ChopTree (Vector3Int p, Players.Player owner)
		{
			return ServerManager.TryChangeBlock(p, 0, owner)
				&& ServerManager.TryChangeBlock(p.Add(0, 1, 0), 0, owner)
				&& ServerManager.TryChangeBlock(p.Add(0, 2, 0), 0, owner);
		}

		#region LOAD_LEGACY_BLOCKS_WORKAROUND
		/// <summary>
		/// This #region code is to load the legacy json data for upgrading from the old area jobs to this
		/// from before v0.5.0 to v0.5.0 and later
		/// </summary>
		JSONNode legacyJSON;

		public override void StartLoading ()
		{
			// do custom things before base.AsyncLoad so FinishLoading also waits for this to complete
			ThreadPool.QueueUserWorkItem(delegate (object obj)
			{
				try {
					string path = string.Format("gamedata/savegames/{0}/blocktypes/ForesterAreaJob.json", ServerManager.WorldName);
					if (File.Exists(path)) {
						Log.Write("Loading legacy json from {0}", path);
						JSON.Deserialize(path, out legacyJSON, false);
						File.Delete(path);
					}
				} catch (System.Exception e) {
					Log.WriteException(e);
				} finally {
					AsyncLoad(obj);
				}
			});
		}

		public override void FinishLoading ()
		{
			base.FinishLoading();
			if (legacyJSON != null) {
				foreach (var pair in legacyJSON.LoopObject()) {
					try {
						Players.Player player = Players.GetPlayer(NetworkID.Parse(pair.Key));

						for (int i = 0; i < pair.Value.ChildCount; i++) {
							JSONNode jobNode = pair.Value[i];

							int npcID = jobNode.GetAsOrDefault("npcID", 0);
							Vector3Int min = (Vector3Int)jobNode["positionMin"];
							Vector3Int max = (Vector3Int)jobNode["positionMax"];

							var job = new DefaultFarmerAreaJob<TemperateForesterDefinition>(player, min, max, npcID);
							if (!AreaJobTracker.RegisterAreaJob(job)) {
								job.OnRemove();
							}
						}
					} catch (System.Exception e) {
						Log.WriteException("Exception loading legacy area job data", e);
					}
				}
				legacyJSON = null;
			}
		}

		#endregion LOAD_LEGACY_BLOCKS_WORKAROUND
	}
}