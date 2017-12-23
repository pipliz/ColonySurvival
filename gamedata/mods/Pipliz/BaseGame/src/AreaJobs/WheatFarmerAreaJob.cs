using BlockTypes.Builtin;
using System.IO;
using System.Threading;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using JSON;

	[AreaJobDefinitionAutoLoader]
	public class WheatFarmerDefinition : AreaJobDefinitionDefault<WheatFarmerDefinition>
	{
		public WheatFarmerDefinition ()
		{
			identifier = "pipliz.wheatfarm";
			fileName = "wheatfarms";
			stages = new ushort[] {
				BuiltinBlocks.WheatStage1,
				BuiltinBlocks.WheatStage2,
				BuiltinBlocks.WheatStage3
			};
			npcType = Server.NPCs.NPCType.GetByKeyNameOrDefault("pipliz.wheatfarmer");
		}

		public override IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			SetLayer(min, max, BuiltinBlocks.Dirt, -1);
			return base.CreateAreaJob(owner, min, max, npcID);
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
					string path = string.Format("gamedata/savegames/{0}/blocktypes/FarmerAreaJob.json", ServerManager.WorldName);
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

							var job = new DefaultFarmerAreaJob<WheatFarmerDefinition>(player, min, max, npcID);
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