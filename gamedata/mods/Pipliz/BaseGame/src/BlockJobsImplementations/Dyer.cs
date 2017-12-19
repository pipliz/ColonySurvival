using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class DyerJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 5f;

		public override string NPCTypeKey { get { return "pipliz.dyer"; } }

		public override float CraftingCooldown
		{
			get { return StaticCraftingCooldown; }
			set { StaticCraftingCooldown = value; }
		}

		public override int MaxRecipeCraftsPerHaul { get { return 4; } }

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Dyer",
				maskColor1 = new UnityEngine.Color32(173, 217, 167, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "dyeing.json");
		}
	}
}
