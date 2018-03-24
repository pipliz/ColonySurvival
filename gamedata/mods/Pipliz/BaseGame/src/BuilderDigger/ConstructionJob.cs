using BlockTypes.Builtin;
using NPC;
using Pipliz.JSON;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pipliz.Mods.BaseGame.Construction
{
	public class ConstructionJob : BlockJobBase, IBlockJobBase, INPCTypeDefiner
	{
		protected ConstructionArea constructionArea;
		protected bool isAreaPresenceTestDone = false;
		protected ushort blockType;
		public int storedItems = 0;

		public override string NPCTypeKey { get { return "pipliz.constructor"; } }

		public override bool NeedsItems { get { return storedItems == 0; } }

		public NPCTypeStandardSettings GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Construction Worker",
				maskColor1 = new Color32(145, 22, 22, 255),
				type = NPCTypeID.GetNextID(),
				inventoryCapacity = 0.1f
			};
		}

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			blockType = ItemTypes.IndexLookup.GetIndex(node.GetAsOrDefault("type", "air"));
			storedItems = node.GetAsOrDefault("storedItems", 0);
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
			JSONNode baseJSON = base.GetJSON();
			if (blockType != 0) {
				baseJSON.SetAs("type", ItemTypes.IndexLookup.GetName(blockType));
			}
			if (storedItems > 0) {
				baseJSON.SetAs("storedItems", storedItems);
			}
			return baseJSON;
		}

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			if (constructionArea != null && !constructionArea.IsValid) {
				constructionArea = null;
			}

			Vector3 pos = usedNPC.Position.Vector;
			if (blockType == 0) {
				World.TryGetTypeAt(position, out blockType);
			}
			if (blockType == BuiltinBlocks.ConstructionJobXP) {
				usedNPC.LookAt(pos + Vector3.right);
			} else if (blockType == BuiltinBlocks.ConstructionJobXN) {
				usedNPC.LookAt(pos + Vector3.left);
			} else if (blockType == BuiltinBlocks.ConstructionJobZP) {
				usedNPC.LookAt(pos + Vector3.forward);
			} else if (blockType == BuiltinBlocks.ConstructionJobZN) {
				usedNPC.LookAt(pos + Vector3.back);
			}

			if (constructionArea == null) {
				List<IAreaJob> jobs;
				if (AreaJobTracker.ExistingAreaAt(position.Add(-1, -1, -1), position.Add(1, 1, 1), out jobs)) {
					for (int i = 0; i < jobs.Count; i++) {
						ConstructionArea neighbourArea = jobs[i] as ConstructionArea;
						if (neighbourArea != null) {
							constructionArea = neighbourArea;
							break;
						}
					}
					AreaJobTracker.AreaJobListPool.Return(jobs);
				}

				if (constructionArea == null) {
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

			Assert.IsNotNull(constructionArea);
			constructionArea.DoJob(this, ref state);
		}

		public override void OnNPCAtStockpile (ref NPCBase.NPCState state)
		{
			storedItems = MaxStoredItems;
			state.JobIsDone = true;
			state.SetCooldown(0.2);
			state.Inventory.Dump(usedNPC.Colony.UsedStockpile);
		}

		public static int MaxStoredItems { get { return 5; } }
	}
}
