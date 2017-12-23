using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class OvenJob : RotatedCraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 8.3f;

		public override string NPCTypeKey { get { return "pipliz.baker"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 3; } }

		public override void OnStartCrafting ()
		{
			base.OnStartCrafting();
			ushort litType;
			if (blockType == BuiltinBlocks.OvenXP) {
				litType = BuiltinBlocks.OvenLitXP;
			} else if (blockType == BuiltinBlocks.OvenXN) {
				litType = BuiltinBlocks.OvenLitXN;
			} else if (blockType == BuiltinBlocks.OvenZP) {
				litType = BuiltinBlocks.OvenLitZP;
			} else if (blockType == BuiltinBlocks.OvenZN) {
				litType = BuiltinBlocks.OvenLitZN;
			} else {
				World.TryGetTypeAt(position, out blockType);
				return;
			}
			blockType = litType;
			ServerManager.TryChangeBlock(position, litType);
		}

		public override void OnStopCrafting ()
		{
			base.OnStopCrafting();

			ushort unLitType;
			if (blockType == BuiltinBlocks.OvenLitXP) {
				unLitType = BuiltinBlocks.OvenXP;
			} else if (blockType == BuiltinBlocks.OvenLitXN) {
				unLitType = BuiltinBlocks.OvenXN;
			} else if (blockType == BuiltinBlocks.OvenLitZP) {
				unLitType = BuiltinBlocks.OvenZP;
			} else if (blockType == BuiltinBlocks.OvenLitZN) {
				unLitType = BuiltinBlocks.OvenZN;
			} else {
				World.TryGetTypeAt(position, out blockType);
				return;
			}
			blockType = unLitType;
			ServerManager.TryChangeBlock(position, unLitType);
		}

		public override Vector3Int GetPositionNPC (Vector3Int position)
		{
			if (blockType == BuiltinBlocks.OvenXP || blockType == BuiltinBlocks.OvenLitXP) {
				return position.Add(1, 0, 0);
			} else if (blockType == BuiltinBlocks.OvenXN || blockType == BuiltinBlocks.OvenLitXN) {
				return position.Add(-1, 0, 0);
			} else if (blockType == BuiltinBlocks.OvenZP || blockType == BuiltinBlocks.OvenLitZP) {
				return position.Add(0, 0, 1);
			} else if (blockType == BuiltinBlocks.OvenZN || blockType == BuiltinBlocks.OvenLitZN) {
				return position.Add(0, 0, -1);
			} else {
				return position;
			}
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Baker",
				maskColor1 = new UnityEngine.Color32(192, 160, 117, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		public override List<string> GetCraftingLimitsTriggers ()
		{
			return new List<string>()
			{
				"ovenx+",
				"ovenx-",
				"ovenz+",
				"ovenz-",
				"ovenlitx+",
				"ovenlitx-",
				"ovenlitz+",
				"ovenlitz-"
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "baking.json");
		}
	}
}
