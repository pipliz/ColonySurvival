using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class FineryForgeJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 6f;
		protected Vector3Int NPCOffset;

		public override string NPCTypeKey { get { return "pipliz.fineryforgejob"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 3; } }

		public override Vector3Int GetJobLocation ()
		{
			return base.GetJobLocation() + NPCOffset;
		}

		public override void OnStartCrafting ()
		{
			base.OnStartCrafting();

			ushort litType;
			if (worldType == BuiltinBlocks.FineryForgeXP) {
				litType = BuiltinBlocks.FineryForgeLitXP;
			} else if (worldType == BuiltinBlocks.FineryForgeXN) {
				litType = BuiltinBlocks.FineryForgeLitXN;
			} else if (worldType == BuiltinBlocks.FineryForgeZP) {
				litType = BuiltinBlocks.FineryForgeLitZP;
			} else if (worldType == BuiltinBlocks.FineryForgeZN) {
				litType = BuiltinBlocks.FineryForgeLitZN;
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
			if (worldType == BuiltinBlocks.FineryForgeLitXP) {
				unLitType = BuiltinBlocks.FineryForgeXP;
			} else if (worldType == BuiltinBlocks.FineryForgeLitXN) {
				unLitType = BuiltinBlocks.FineryForgeXN;
			} else if (worldType == BuiltinBlocks.FineryForgeLitZP) {
				unLitType = BuiltinBlocks.FineryForgeZP;
			} else if (worldType == BuiltinBlocks.FineryForgeLitZN) {
				unLitType = BuiltinBlocks.FineryForgeZN;
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
			if (type == BuiltinBlocks.FineryForgeLitXP || type == BuiltinBlocks.FineryForgeXP) {
				NPCOffset = new Vector3Int(1, 0, 0);
			} else if (type == BuiltinBlocks.FineryForgeLitXN || type == BuiltinBlocks.FineryForgeXN) {
				NPCOffset = new Vector3Int(-1, 0, 0);
			} else if (type == BuiltinBlocks.FineryForgeLitZP || type == BuiltinBlocks.FineryForgeZP) {
				NPCOffset = new Vector3Int(0, 0, 1);
			} else if (type == BuiltinBlocks.FineryForgeLitZN || type == BuiltinBlocks.FineryForgeZN) {
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
				printName = "Steel smelter",
				maskColor1 = new UnityEngine.Color32(139, 139, 139, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		public override List<string> GetCraftingLimitsTriggers ()
		{
			return new List<string>()
			{
				"fineryforgex+",
				"fineryforgex-",
				"fineryforgez+",
				"fineryforgez-",
				"fineryforgelitx+",
				"fineryforgelitx-",
				"fineryforgelitz+",
				"fineryforgelitz-"
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "fineryforge.json");
		}
	}
}
