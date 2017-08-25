using BlockTypes.Builtin;
using System.Collections.Generic;
using NPC;
using Pipliz.APIProvider.Jobs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class FurnaceJob : FueledCraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.smelter"; } }

		public override float TimeBetweenJobs { get { return 7.5f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 2; } }

		public override void OnLit ()
		{
			ushort litType;
			if (blockType == BuiltinBlocks.FurnaceUnlitXP) {
				litType = BuiltinBlocks.FurnaceLitXP;
			} else if (blockType == BuiltinBlocks.FurnaceUnlitXN) {
				litType = BuiltinBlocks.FurnaceLitXN;
			} else if (blockType == BuiltinBlocks.FurnaceUnlitZP) {
				litType = BuiltinBlocks.FurnaceLitZP;
			} else {
				litType = BuiltinBlocks.FurnaceLitZN;
			}
			ServerManager.TryChangeBlock(position, litType);
		}

		public override Vector3Int GetPositionNPC (Vector3Int position)
		{
			Vector3Int positionNPC;
			if (blockType == BuiltinBlocks.FurnaceLitXP || blockType == BuiltinBlocks.FurnaceUnlitXP) {
				positionNPC = position.Add(1, 0, 0);
			} else if (blockType == BuiltinBlocks.FurnaceLitXN || blockType == BuiltinBlocks.FurnaceUnlitXN) {
				positionNPC = position.Add(-1, 0, 0);
			} else if (blockType == BuiltinBlocks.FurnaceLitZP || blockType == BuiltinBlocks.FurnaceUnlitZP) {
				positionNPC = position.Add(0, 0, 1);
			} else if (blockType == BuiltinBlocks.FurnaceLitZN || blockType == BuiltinBlocks.FurnaceUnlitZN) {
				positionNPC = position.Add(0, 0, -1);
			} else {
				positionNPC = position;
			}
			return positionNPC;
		}

		NPCTypeSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			NPCTypeSettings def = NPCTypeSettings.Default;
			def.keyName = NPCTypeKey;
			def.printName = "Smelter";
			def.maskColor1 = new UnityEngine.Color32(155, 100, 91, 255);
			def.type = NPCTypeID.GetNextID();
			return def;
		}

		public override List<string> GetCraftingLimitsTriggers ()
		{
			return new List<string>()
			{
				"furnacex+",
				"furnacex-",
				"furnacez+",
				"furnacez-",
				"furnacelitx+",
				"furnacelitx-",
				"furnacelitz+",
				"furnacelitz-"
			};
		}
	}
}
