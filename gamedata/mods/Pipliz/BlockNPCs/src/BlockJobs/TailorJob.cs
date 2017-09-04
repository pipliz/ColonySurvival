using NPC;
using Pipliz.APIProvider.Jobs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class TailorJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.tailor"; } }

		public override float TimeBetweenJobs { get { return 6f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 2; } }

		NPCTypeSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			NPCTypeSettings def = NPCTypeSettings.Default;
			def.keyName = NPCTypeKey;
			def.printName = "Tailor";
			def.maskColor1 = new UnityEngine.Color32(50, 96, 117, 255);
			def.type = NPCTypeID.GetNextID();
			return def;
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "tailoring.json");
		}
	}
}
