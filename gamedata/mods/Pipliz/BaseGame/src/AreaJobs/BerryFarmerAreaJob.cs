using BlockTypes;
using NPC;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using Areas;

	[AreaJobDefinitionAutoLoader]
	public class BerryFarmerDefinition : AreaJobDefinitionDefault<BerryFarmerDefinition>
	{
		public BerryFarmerDefinition ()
		{
			identifier = "pipliz.berryfarm";
			fileName = "berryfarms";
			npcType = NPCType.GetByKeyNameOrDefault("pipliz.berryfarmer");
			areaType = Shared.EAreaType.BerryFarm;
		}

		/// Override it to use custom berryfarmerjob, to store some per-job data
		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			return new BerryFarmerJob(owner, min, max, npcID);
		}

		public override void CalculateSubPosition (IAreaJob rawJob, ref Vector3Int positionSub)
		{
			BerryFarmerJob job = (BerryFarmerJob)rawJob;

			Vector3Int min = job.Minimum;
			Vector3Int max = job.Maximum;

			if (job.checkMissingBushes && job.NPC.Colony.Stockpile.Contains(BuiltinBlocks.BerryBush)) {
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
							positionSub = AI.AIManager.ClosestPosition(job.bushLocation, job.NPC.Position);
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
							positionSub = AI.AIManager.ClosestPositionNotAt(job.bushLocation, job.NPC.Position);
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

		static System.Collections.Generic.List<ItemTypes.ItemTypeDrops> GatherResults = new System.Collections.Generic.List<ItemTypes.ItemTypeDrops>();

		public override void OnNPCAtJob (IAreaJob rawJob, ref Vector3Int positionSub, ref NPCBase.NPCState state, ref bool shouldDumpInventory)
		{
			BerryFarmerJob job = (BerryFarmerJob)rawJob;

			state.JobIsDone = true;
			if (positionSub.IsValid) {
				ushort type;
				if (job.placingMissingBush) {
					if (job.NPC.Colony.Stockpile.TryRemove(BuiltinBlocks.BerryBush)) {
						job.placingMissingBush = false;
						// todo use colony as param
						ServerManager.TryChangeBlock(job.bushLocation, BuiltinBlocks.BerryBush, rawJob.Owner.Owners[0], ServerManager.SetBlockFlags.DefaultAudio);
						state.SetCooldown(2.0);
					} else {
						state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(8f, 14f), BuiltinBlocks.BerryBush, true, false));
					}
				} else if (job.removingOldBush) {
					// todo use colony as param
					if (ServerManager.TryChangeBlock(job.bushLocation, 0, rawJob.Owner.Owners[0], ServerManager.SetBlockFlags.DefaultAudio)) {
						job.NPC.Colony.Stockpile.Add(BuiltinBlocks.BerryBush);
						job.removingOldBush = false;
					}
					state.SetCooldown(2.0);
				} else if (World.TryGetTypeAt(positionSub, out type)) {
					if (type == 0) {
						job.checkMissingBushes = true;
						state.SetCooldown(1.0, 4.0);
					} else if (type == BuiltinBlocks.BerryBush) {
						GatherResults.Clear();
						GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.Berry, 1, 1.0));
						GatherResults.Add(new ItemTypes.ItemTypeDrops(BuiltinBlocks.BerryBush, 1, 0.1));

						ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCGathered, rawJob as IJob, positionSub, GatherResults);

						InventoryItem toShow = ItemTypes.ItemTypeDrops.GetWeightedRandom(GatherResults);
						if (toShow.Amount > 0) {
							state.SetIndicator(new Shared.IndicatorState(8.5f, toShow.Type));
						} else {
							state.SetCooldown(8.5);
						}

						job.NPC.Inventory.Add(GatherResults);
					} else {
						state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(8f, 14f), BuiltinBlocks.ErrorMissing));
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

			public BerryFarmerJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0) : base(owner, min, max, npcID)
			{

			}
		}
	}
}