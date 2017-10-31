using Pipliz.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class WoodcutterJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.woodcutter"; } }

		public override float TimeBetweenJobs { get { return 4f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 5; } }

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Woodcutter",
				maskColor1 = new UnityEngine.Color32(116, 66, 43, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override void OnRecipeCrafted ()
		{
			base.OnRecipeCrafted();
			ServerManager.SendAudio(position.Vector, "woodCut");
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "woodcutting.json");
		}
	}
}
