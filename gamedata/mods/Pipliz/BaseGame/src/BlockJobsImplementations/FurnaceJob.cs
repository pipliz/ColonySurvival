using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class FurnaceJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 7.5f;
		protected Vector3Int NPCOffset;

		public override string NPCTypeKey { get { return "pipliz.smelter"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 2; } }

		public override Vector3Int GetJobLocation ()
		{
			return base.GetJobLocation() + NPCOffset;
		}

		public override void OnStartCrafting ()
		{
			base.OnStartCrafting();

			ushort litType;
			if (worldType == BuiltinBlocks.FurnaceXP) {
				litType = BuiltinBlocks.FurnaceLitXP;
			} else if (worldType == BuiltinBlocks.FurnaceXN) {
				litType = BuiltinBlocks.FurnaceLitXN;
			} else if (worldType == BuiltinBlocks.FurnaceZP) {
				litType = BuiltinBlocks.FurnaceLitZP;
			} else if (worldType == BuiltinBlocks.FurnaceZN) {
				litType = BuiltinBlocks.FurnaceLitZN;
			} else {
				CheckWorldType();
				return;
			}

			if (ServerManager.TryChangeBlock(position, litType)) {
				worldType = litType;
			}
		}

		public override void OnStopCrafting ()
		{
			base.OnStopCrafting();

			ushort unLitType;
			if (worldType == BuiltinBlocks.FurnaceLitXP) {
				unLitType = BuiltinBlocks.FurnaceXP;
			} else if (worldType == BuiltinBlocks.FurnaceLitXN) {
				unLitType = BuiltinBlocks.FurnaceXN;
			} else if (worldType == BuiltinBlocks.FurnaceLitZP) {
				unLitType = BuiltinBlocks.FurnaceZP;
			} else if (worldType == BuiltinBlocks.FurnaceLitZN) {
				unLitType = BuiltinBlocks.FurnaceZN;
			} else {
				CheckWorldType();
				return;
			}

			if (ServerManager.TryChangeBlock(position, unLitType)) {
				worldType = unLitType;
			}
		}

		protected override bool IsValidWorldType (ushort type)
		{
			if (type == BuiltinBlocks.FurnaceLitXP || type == BuiltinBlocks.FurnaceXP) {
				NPCOffset = new Vector3Int(1, 0, 0);
			} else if (type == BuiltinBlocks.FurnaceLitXN || type == BuiltinBlocks.FurnaceXN) {
				NPCOffset = new Vector3Int(-1, 0, 0);
			} else if (type == BuiltinBlocks.FurnaceLitZP || type == BuiltinBlocks.FurnaceZP) {
				NPCOffset = new Vector3Int(0, 0, 1);
			} else if (type == BuiltinBlocks.FurnaceLitZN || type == BuiltinBlocks.FurnaceZN) {
				NPCOffset = new Vector3Int(0, 0, -1);
			} else {
				return false;
			}
			return true;
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Smelter",
				maskColor1 = new UnityEngine.Color32(155, 100, 91, 255),
				type = NPCTypeID.GetNextID()
			};
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

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "smelting.json");
		}
	}
}
