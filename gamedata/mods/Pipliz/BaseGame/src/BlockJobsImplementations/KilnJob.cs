using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class KilnJob : RotatedCraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 6f;

		public override string NPCTypeKey { get { return "pipliz.kilnjob"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 3; } }

		public override Vector3Int GetPositionNPC (Vector3Int position)
		{
			if (blockType == BuiltinBlocks.KilnXP) {
				return position.Add(1, 0, 0);
			} else if (blockType == BuiltinBlocks.KilnXN) {
				return position.Add(-1, 0, 0);
			} else if (blockType == BuiltinBlocks.KilnZP) {
				return position.Add(0, 0, 1);
			} else if (blockType == BuiltinBlocks.KilnZN) {
				return position.Add(0, 0, -1);
			} else {
				World.TryGetTypeAt(position, out blockType);
				return position;
			}
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
