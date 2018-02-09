using BlockTypes.Builtin;
using NPC;
using Pipliz.JSON;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class DiggerJob : BlockJobBase, IBlockJobBase, INPCTypeDefiner
	{
		protected AreaJobs.DiggerAreaJob diggerArea;
		protected bool isAreaPresenceTestDone = false;
		protected ushort blockType;

		public override string NPCTypeKey { get { return "pipliz.bloomeryjob"; } }

		public NPCTypeStandardSettings GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "DiggerDude",
				maskColor1 = new UnityEngine.Color32(255, 255, 0, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			blockType = ItemTypes.IndexLookup.GetIndex(node.GetAsOrDefault("type", "air"));
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			return this;
		}

		public ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			blockType = type;
			InitializeJob(player, position, 0);
			return this;
		}

		public override JSONNode GetJSON ()
		{
			return base.GetJSON()
				.SetAs("type", ItemTypes.IndexLookup.GetName(blockType));
		}

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			if (diggerArea != null && !diggerArea.IsValid) {
				diggerArea = null;
			}

			Vector3 pos = usedNPC.Position.Vector;
			if (blockType == 0) {
				World.TryGetTypeAt(position, out blockType);
			}
			if (blockType == BuiltinBlocks.DiggerJobXP) {
				usedNPC.LookAt(pos + Vector3.right);
			} else if (blockType == BuiltinBlocks.DiggerJobXN) {
				usedNPC.LookAt(pos + Vector3.left);
			} else if (blockType == BuiltinBlocks.DiggerJobZP) {
				usedNPC.LookAt(pos + Vector3.forward);
			} else if (blockType == BuiltinBlocks.DiggerJobZN) {
				usedNPC.LookAt(pos + Vector3.back);
			}

			if (diggerArea == null) {
				List<IAreaJob> jobs;
				if (AreaJobTracker.ExistingAreaAt(position.Add(-1, -1, -1), position.Add(1, 1, 1), out jobs)) {
					for (int i = 0; i < jobs.Count; i++) {
						AreaJobs.DiggerAreaJob diggerAreaTest = jobs[i] as AreaJobs.DiggerAreaJob;
						if (diggerAreaTest != null) {
							diggerArea = diggerAreaTest;
							break;
						}
					}
					AreaJobTracker.AreaJobListPool.Return(jobs);
				}

				if (diggerArea == null) {
					if (isAreaPresenceTestDone) {
						state.SetCooldown(0.5);
						ServerManager.TryChangeBlock(position, 0);
					} else {
						state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(3f, 5f), BuiltinBlocks.ErrorIdle));
						isAreaPresenceTestDone = true;
					}
					return;
				}
			}

			Assert.IsNotNull(diggerArea);
			diggerArea.DoJob(this, ref state);
		}
	}
}
