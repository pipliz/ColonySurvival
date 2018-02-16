using BlockTypes.Builtin;
using NPC;
using Pipliz.Mods.APIProvider.Jobs;
using Pipliz.JSON;
using Server.NPCs;
using UnityEngine;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class MinerJob : BlockJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public ushort type;
		public ushort typeBelow;
		public float cooldown = 8f;

		public override string NPCTypeKey { get { return "pipliz.minerjob"; } }

		public virtual float MiningCooldown
		{
			get { return cooldown; }
			set { cooldown = value; }
		}

		public override InventoryItem RecruitementItem { get { return new InventoryItem(BuiltinBlocks.BronzePickaxe, 1); } }

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			type = ItemTypes.IndexLookup.GetOrGenerate(node.GetAs<string>("type"));
			typeBelow = ItemTypes.IndexLookup.GetOrGenerate(node.GetAs<string>("typeBelow"));
			JSONNode customDataNode = ItemTypes.GetType(typeBelow).CustomDataNode;
			if (customDataNode != null) {
				MiningCooldown = customDataNode.GetAsOrDefault("minerMiningTime", 8f);
			}
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			return this;
		}

		public ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			this.type = type;
			if (World.TryGetTypeAt(position.Add(0, -1, 0), out typeBelow)) {
				JSONNode customDataNode = ItemTypes.GetType(typeBelow).CustomDataNode;
				if (customDataNode != null) {
					MiningCooldown = customDataNode.GetAsOrDefault("minerMiningTime", 8f);
				}
			}
			InitializeJob(player, position, 0);
			return this;
		}

		public override JSONNode GetJSON ()
		{
			return base.GetJSON()
				.SetAs("typeBelow", ItemTypes.IndexLookup.GetName(typeBelow))
				.SetAs("type", ItemTypes.IndexLookup.GetName(type));
		}

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			Vector3 rotate = usedNPC.Position.Vector;
			if (type == BuiltinBlocks.MinerJobXN) {
				rotate += Vector3.left;
			} else if (type == BuiltinBlocks.MinerJobXP) {
				rotate += Vector3.right;
			} else if (type == BuiltinBlocks.MinerJobZP) {
				rotate += Vector3.forward;
			} else if (type == BuiltinBlocks.MinerJobZN) {
				rotate += Vector3.back;
			}
			usedNPC.LookAt(rotate);

			ServerManager.SendAudio(position.Vector, "stoneDelete");
			var itemList = ItemTypes.GetType(typeBelow).OnRemoveItems;
			state.Inventory.Add(itemList);
			state.JobIsDone = true;
			state.SetIndicator(new Shared.IndicatorState(MiningCooldown, itemList[0].item.Type));
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Miner",
				maskColor1 = new Color32(63, 57, 57, 255),
				type = NPCTypeID.GetNextID(),
				inventoryCapacity = 0.1f
			};
		}
	}
}
