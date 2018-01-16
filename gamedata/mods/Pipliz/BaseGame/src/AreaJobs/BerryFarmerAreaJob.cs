using BlockTypes.Builtin;
using NPC;
using System.IO;
using System.Threading;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using JSON;

	[AreaJobDefinitionAutoLoader]
	public class BerryFarmerDefinition : AreaJobDefinitionDefault<BerryFarmerDefinition>
	{
		public BerryFarmerDefinition ()
		{
			identifier = "pipliz.berryfarm";
			fileName = "berryfarms";
			npcType = Server.NPCs.NPCType.GetByKeyNameOrDefault("pipliz.berryfarmer");
		}

		/// Override it to use custom berryfarmerjob, to store some per-job data
		public override IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			return new BerryFarmerJob(owner, min, max, npcID);
		}

		public override void CalculateSubPosition (IAreaJob rawJob, ref Vector3Int positionSub)
		{
			BerryFarmerJob job = (BerryFarmerJob)rawJob;

			Vector3Int min = job.Minimum;
			Vector3Int max = job.Maximum;

			if (job.checkMissingBushes && job.UsedNPC.Colony.UsedStockpile.Contains(BuiltinBlocks.BerryBush)) {
				// remove legacy positions
				for (int x = min.x + 1; x <= max.x; x += 2) {
					for (int z = min.z; z <= max.z; z += 2) {
						ushort type;
						Vector3Int possiblePositionSub = new Vector3Int(x, min.y, z);
						if (!World.TryGetTypeAt(possiblePositionSub, out type)) {
							return;
						}
						if (type == BuiltinBlocks.BerryBush) {
							job.removingOldBush = true;
							job.bushLocation = possiblePositionSub;
							positionSub = Server.AI.AIManager.ClosestPosition(job.bushLocation, job.UsedNPC.Position);
							return;
						}
					}
				}
				// place new positions
				for (int x = min.x; x <= max.x; x += 2) {
					for (int z = min.z; z <= max.z; z += 2) {
						ushort type;
						Vector3Int possiblePositionSub = new Vector3Int(x, min.y, z);
						if (!World.TryGetTypeAt(possiblePositionSub, out type)) {
							return;
						}
						if (type == 0) {
							job.placingMissingBush = true;
							job.bushLocation = possiblePositionSub;
							positionSub = Server.AI.AIManager.ClosestPositionNotAt(job.bushLocation, job.UsedNPC.Position);
							return;
						}
					}
				}
				job.checkMissingBushes = false;
			}

			positionSub = min;
			positionSub.x += Random.Next(0, (max.x - min.x) / 2 + 1) * 2;
			positionSub.z += Random.Next(0, (max.z - min.z) / 2 + 1) * 2;
		}

		public override void OnNPCAtJob (IAreaJob rawJob, ref Vector3Int positionSub, ref NPCBase.NPCState state, ref bool shouldDumpInventory)
		{
			BerryFarmerJob job = (BerryFarmerJob)rawJob;

			state.JobIsDone = true;
			if (positionSub.IsValid) {
				ushort type;
				if (job.placingMissingBush) {
					if (job.UsedNPC.Colony.UsedStockpile.TryRemove(BuiltinBlocks.BerryBush)) {
						job.placingMissingBush = false;
						ServerManager.TryChangeBlock(job.bushLocation, BuiltinBlocks.BerryBush, ServerManager.SetBlockFlags.DefaultAudio);
						state.SetCooldown(2.0);
					} else {
						state.SetIndicator(NPCIndicatorType.MissingItem, Random.NextFloat(8f, 14f), BuiltinBlocks.BerryBush);
					}
				} else if (job.removingOldBush) {
					if (ServerManager.TryChangeBlock(job.bushLocation, 0, ServerManager.SetBlockFlags.DefaultAudio)) {
						job.UsedNPC.Colony.UsedStockpile.Add(BuiltinBlocks.BerryBush);
						job.removingOldBush = false;
					}
					state.SetCooldown(2.0);
				} else if (World.TryGetTypeAt(positionSub, out type)) {
					if (type == 0) {
						job.checkMissingBushes = true;
						state.SetCooldown(1.0, 4.0);
					} else if (type == BuiltinBlocks.BerryBush) {
						state.SetIndicator(NPCIndicatorType.Crafted, 8.5f, BuiltinBlocks.Berry);
						NPCInventory inv = job.UsedNPC.Inventory;
						inv.Add(BuiltinBlocks.Berry);
						if (Random.Next(0, 10) >= 9) {
							inv.Add(BuiltinBlocks.BerryBush);
						}
					} else {
						state.SetIndicator(NPCIndicatorType.MissingItem, Random.NextFloat(8f, 14f), BuiltinBlocks.ErrorMissing);
					}
				} else {
					state.SetCooldown(Random.NextFloat(3f, 6f));
				}
				positionSub = Vector3Int.invalidPos;
			} else {
				state.SetCooldown(10.0);
			}
		}

		/// <summary>
		/// Simple wrapper to have some per-job data
		/// </summary>
		class BerryFarmerJob : DefaultFarmerAreaJob<BerryFarmerDefinition>
		{
			public Vector3Int bushLocation = Vector3Int.invalidPos;
			public bool checkMissingBushes = true;
			public bool placingMissingBush = false;
			public bool removingOldBush = false;

			public BerryFarmerJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(owner, min, max, npcID)
			{

			}
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
					string path = string.Format("gamedata/savegames/{0}/blocktypes/BerryAreaJob.json", ServerManager.WorldName);
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

							var job = new DefaultFarmerAreaJob<BerryFarmerDefinition>(player, min, max, npcID);
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