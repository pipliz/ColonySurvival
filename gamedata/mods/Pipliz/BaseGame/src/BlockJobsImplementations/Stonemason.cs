using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class StonemasonJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 5f;

		public override string NPCTypeKey { get { return "pipliz.stonemason"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 3; } }

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Stonemason",
				maskColor1 = new UnityEngine.Color32(163, 163, 163, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override bool IsValidWorldType (ushort type)
		{
			return type == BuiltinBlocks.StoneMasonWorkbench;
		}
	}
}
