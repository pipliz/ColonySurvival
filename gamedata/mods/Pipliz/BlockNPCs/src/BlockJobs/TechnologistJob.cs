using NPC;
using Pipliz.APIProvider.Jobs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class TechnologistJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.technologist"; } }

		public override float TimeBetweenJobs { get { return 10f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 2; } }

		NPCTypeSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			NPCTypeSettings def = NPCTypeSettings.Default;
			def.keyName = NPCTypeKey;
			def.printName = "Technologist";
			def.maskColor1 = new UnityEngine.Color32(28, 26, 26, 255);
			def.type = NPCTypeID.GetNextID();
			return def;
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "technologist.json");
		}
	}
}
