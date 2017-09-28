using Pipliz.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GunSmithJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.gunsmithjob"; } }

		public override float TimeBetweenJobs { get { return 6f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 3; } }

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Gun smith",
				maskColor1 = new UnityEngine.Color32(255, 0, 255, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "gunsmith.json");
		}
	}
}
