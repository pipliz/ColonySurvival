using Pipliz.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class WorkBenchJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.crafter"; } }

		public override float TimeBetweenJobs { get { return 8f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 5; } }

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Crafter",
				maskColor1 = new UnityEngine.Color32(81, 121, 123, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override void OnRecipeCrafted ()
		{
			base.OnRecipeCrafted();
			ServerManager.SendAudio(position.Vector, "crafting");
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "crafting.json");
		}
	}
}
