using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class KilnJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 6f;
		protected Vector3Int NPCOffset;

		public override string NPCTypeKey { get { return "pipliz.kilnjob"; } }

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

		protected override bool IsValidWorldType (ushort type)
		{
			if (type == BuiltinBlocks.KilnXP) {
				NPCOffset = new Vector3Int(1, 0, 0);
			} else if (type == BuiltinBlocks.KilnXN) {
				NPCOffset = new Vector3Int(-1, 0, 0);
			} else if (type == BuiltinBlocks.KilnZP) {
				NPCOffset = new Vector3Int(0, 0, 1);
			} else if (type == BuiltinBlocks.KilnZN) {
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
				printName = "Charcoal burner",
				maskColor1 = new UnityEngine.Color32(74, 100, 184, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		public override List<string> GetCraftingLimitsTriggers ()
		{
			return new List<string>()
			{
				"kilnx+",
				"kilnx-",
				"kilnz+",
				"kilnz-"
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "kiln.json");
		}
	}
}
