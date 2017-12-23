using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class FurnaceJob : RotatedCraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 7.5f;

		public override string NPCTypeKey { get { return "pipliz.smelter"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 2; } }

		public override void OnStartCrafting ()
		{
			base.OnStartCrafting();

			ushort litType;
			if (blockType == BuiltinBlocks.FurnaceXP) {
				litType = BuiltinBlocks.FurnaceLitXP;
			} else if (blockType == BuiltinBlocks.FurnaceXN) {
				litType = BuiltinBlocks.FurnaceLitXN;
			} else if (blockType == BuiltinBlocks.FurnaceZP) {
				litType = BuiltinBlocks.FurnaceLitZP;
			} else if (blockType == BuiltinBlocks.FurnaceZN) {
				litType = BuiltinBlocks.FurnaceLitZN;
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
			if (blockType == BuiltinBlocks.FurnaceLitXP) {
				unLitType = BuiltinBlocks.FurnaceXP;
			} else if (blockType == BuiltinBlocks.FurnaceLitXN) {
				unLitType = BuiltinBlocks.FurnaceXN;
			} else if (blockType == BuiltinBlocks.FurnaceLitZP) {
				unLitType = BuiltinBlocks.FurnaceZP;
			} else if (blockType == BuiltinBlocks.FurnaceLitZN) {
				unLitType = BuiltinBlocks.FurnaceZN;
			} else {
				World.TryGetTypeAt(position, out blockType);
				return;
			}
			blockType = unLitType;
			ServerManager.TryChangeBlock(position, unLitType);
		}

		public override Vector3Int GetPositionNPC (Vector3Int position)
		{
			if (blockType == BuiltinBlocks.FurnaceLitXP || blockType == BuiltinBlocks.FurnaceXP) {
				return position.Add(1, 0, 0);
			} else if (blockType == BuiltinBlocks.FurnaceLitXN || blockType == BuiltinBlocks.FurnaceXN) {
				return position.Add(-1, 0, 0);
			} else if (blockType == BuiltinBlocks.FurnaceLitZP || blockType == BuiltinBlocks.FurnaceZP) {
				return position.Add(0, 0, 1);
			} else if (blockType == BuiltinBlocks.FurnaceLitZN || blockType == BuiltinBlocks.FurnaceZN) {
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
