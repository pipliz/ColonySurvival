using NPC;
using Pipliz.APIProvider.Jobs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class WorkBenchJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.crafter"; } }

		public override float TimeBetweenJobs { get { return 5f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 5; } }

		NPCTypeSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			NPCTypeSettings def = NPCTypeSettings.Default;
			def.keyName = NPCTypeKey;
			def.printName = "Crafter";
			def.maskColor1 = new UnityEngine.Color32(81, 121, 123, 255);
			def.type = NPCTypeID.GetNextID();
			return def;
		}
	}
}
