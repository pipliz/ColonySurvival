﻿using NPC;
using Pipliz.APIProvider.Jobs;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GrinderJob : CraftingJobBase, IBlockJobBase, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.grinder"; } }

		public override float TimeBetweenJobs { get { return 2.9f; } }

		public override int MaxRecipeCraftsPerHaul { get { return 6; } }

		NPCTypeSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			NPCTypeSettings def = NPCTypeSettings.Default;
			def.keyName = NPCTypeKey;
			def.printName = "Grinder";
			def.maskColor1 = new UnityEngine.Color32(87, 66, 41, 255);
			def.type = NPCTypeID.GetNextID();
			return def;
		}

		protected override string GetRecipeLocation ()
		{
			return System.IO.Path.Combine(ModEntries.ModGamedataDirectory, "grinding.json");
		}
	}
}
