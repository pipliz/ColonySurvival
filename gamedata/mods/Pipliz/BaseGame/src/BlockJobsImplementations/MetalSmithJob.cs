using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class MetalSmithJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public static float StaticCraftingCooldown = 5f;

		public override string NPCTypeKey { get { return "pipliz.metalsmithjob"; } }

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
				printName = "Metal smith",
				maskColor1 = new UnityEngine.Color32(170, 77, 13, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override void OnRecipeCrafted ()
		{
			base.OnRecipeCrafted();
			ServerManager.SendAudio(position.Vector, "anvil");
		}

		protected override bool IsValidWorldType (ushort type)
		{
			return type == BuiltinBlocks.BronzeAnvil;
		}
	}
}
