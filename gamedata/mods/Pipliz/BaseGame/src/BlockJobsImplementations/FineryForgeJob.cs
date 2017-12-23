using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class FineryForgeJob : RotatedCraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 6f;

		public override string NPCTypeKey { get { return "pipliz.fineryforgejob"; } }

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
			if (blockType == BuiltinBlocks.FineryForgeXP) {
				litType = BuiltinBlocks.FineryForgeLitXP;
			} else if (blockType == BuiltinBlocks.FineryForgeXN) {
				litType = BuiltinBlocks.FineryForgeLitXN;
			} else if (blockType == BuiltinBlocks.FineryForgeZP) {
				litType = BuiltinBlocks.FineryForgeLitZP;
			} else if (blockType == BuiltinBlocks.FineryForgeZN) {
				litType = BuiltinBlocks.FineryForgeLitZN;
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
			if (blockType == BuiltinBlocks.FineryForgeLitXP) {
				unLitType = BuiltinBlocks.FineryForgeXP;
			} else if (blockType == BuiltinBlocks.FineryForgeLitXN) {
				unLitType = BuiltinBlocks.FineryForgeXN;
			} else if (blockType == BuiltinBlocks.FineryForgeLitZP) {
				unLitType = BuiltinBlocks.FineryForgeZP;
			} else if (blockType == BuiltinBlocks.FineryForgeLitZN) {
				unLitType = BuiltinBlocks.FineryForgeZN;
			} else {
				World.TryGetTypeAt(position, out blockType);
				return;
			}
			blockType = unLitType;
			ServerManager.TryChangeBlock(position, unLitType);
		}

		public override Vector3Int GetPositionNPC (Vector3Int position)
		{
			if (blockType == BuiltinBlocks.FineryForgeLitXP || blockType == BuiltinBlocks.FineryForgeXP) {
				return position.Add(1, 0, 0);
			} else if (blockType == BuiltinBlocks.FineryForgeLitXN || blockType == BuiltinBlocks.FineryForgeXN) {
				return position.Add(-1, 0, 0);
			} else if (blockType == BuiltinBlocks.FineryForgeLitZP || blockType == BuiltinBlocks.FineryForgeZP) {
				return position.Add(0, 0, 1);
			} else if (blockType == BuiltinBlocks.FineryForgeLitZN || blockType == BuiltinBlocks.FineryForgeZN) {
				return position.Add(0, 0, -1);
			} else {
				Log.Write("Unexpect blocktype {0} for job {1} at {2}", ItemTypes.IndexLookup.GetName(blockType), NPCTypeKey, position);
				return position;
			}
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
