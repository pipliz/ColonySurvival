using Pipliz.APIProvider.Jobs;
using Server.NPCs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class ShopJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.merchant"; } }

		public override float TimeBetweenJobs { get { return 10f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 1; } }

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Merchant",
				maskColor1 = new UnityEngine.Color32(146, 31, 31, 255),
				type = NPCTypeID.GetNextID()
			};
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "shopping.json");
		}
	}
}
