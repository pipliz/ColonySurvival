using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class OvenJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 8.3f;
		protected Vector3Int NPCOffset;

		public override string NPCTypeKey { get { return "pipliz.baker"; } }

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
			if (worldType == BuiltinBlocks.OvenXP) {
				litType = BuiltinBlocks.OvenLitXP;
			} else if (worldType == BuiltinBlocks.OvenXN) {
				litType = BuiltinBlocks.OvenLitXN;
			} else if (worldType == BuiltinBlocks.OvenZP) {
				litType = BuiltinBlocks.OvenLitZP;
			} else if (worldType == BuiltinBlocks.OvenZN) {
				litType = BuiltinBlocks.OvenLitZN;
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
			if (worldType == BuiltinBlocks.OvenLitXP) {
				unLitType = BuiltinBlocks.OvenXP;
			} else if (worldType == BuiltinBlocks.OvenLitXN) {
				unLitType = BuiltinBlocks.OvenXN;
			} else if (worldType == BuiltinBlocks.OvenLitZP) {
				unLitType = BuiltinBlocks.OvenZP;
			} else if (worldType == BuiltinBlocks.OvenLitZN) {
				unLitType = BuiltinBlocks.OvenZN;
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
			if (type == BuiltinBlocks.OvenLitXP || type == BuiltinBlocks.OvenXP) {
				NPCOffset = new Vector3Int(1, 0, 0);
			} else if (type == BuiltinBlocks.OvenLitXN || type == BuiltinBlocks.OvenXN) {
				NPCOffset = new Vector3Int(-1, 0, 0);
			} else if (type == BuiltinBlocks.OvenLitZP || type == BuiltinBlocks.OvenZP) {
				NPCOffset = new Vector3Int(0, 0, 1);
			} else if (type == BuiltinBlocks.OvenLitZN || type == BuiltinBlocks.OvenZN) {
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
				printName = "Baker",
				maskColor1 = new UnityEngine.Color32(192, 160, 117, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
