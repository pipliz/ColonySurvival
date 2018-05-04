using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class BloomeryJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 6f;
		protected Vector3Int NPCOffset;

		public override string NPCTypeKey { get { return "pipliz.bloomeryjob"; } }

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
			if (worldType == BuiltinBlocks.BloomeryXP) {
				litType = BuiltinBlocks.BloomeryLitXP;
			} else if (worldType == BuiltinBlocks.BloomeryXN) {
				litType = BuiltinBlocks.BloomeryLitXN;
			} else if (worldType == BuiltinBlocks.BloomeryZP) {
				litType = BuiltinBlocks.BloomeryLitZP;
			} else if (worldType == BuiltinBlocks.BloomeryZN) {
				litType = BuiltinBlocks.BloomeryLitZN;
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
			if (worldType == BuiltinBlocks.BloomeryLitXP) {
				unLitType = BuiltinBlocks.BloomeryXP;
			} else if (worldType == BuiltinBlocks.BloomeryLitXN) {
				unLitType = BuiltinBlocks.BloomeryXN;
			} else if (worldType == BuiltinBlocks.BloomeryLitZP) {
				unLitType = BuiltinBlocks.BloomeryZP;
			} else if (worldType == BuiltinBlocks.BloomeryLitZN) {
				unLitType = BuiltinBlocks.BloomeryZN;
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
			if (type == BuiltinBlocks.BloomeryLitXP || type == BuiltinBlocks.BloomeryXP) {
				NPCOffset = new Vector3Int(1, 0, 0);
			} else if (type == BuiltinBlocks.BloomeryLitXN || type == BuiltinBlocks.BloomeryXN) {
				NPCOffset = new Vector3Int(-1, 0, 0);
			} else if (type == BuiltinBlocks.BloomeryLitZP || type == BuiltinBlocks.BloomeryZP) {
				NPCOffset = new Vector3Int(0, 0, 1);
			} else if (type == BuiltinBlocks.BloomeryLitZN || type == BuiltinBlocks.BloomeryZN) {
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
				printName = "Iron smelter",
				maskColor1 = new UnityEngine.Color32(140, 72, 49, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
