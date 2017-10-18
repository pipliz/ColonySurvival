using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardCrossbowJobNight : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardcrossbownight"; } }

		protected override GuardSettings SetupSettings ()
		{
			GuardSettings set = new GuardSettings();
			set.cooldownMissingItem = 1.5f;
			set.cooldownSearchingTarget = 0.3f;
			set.cooldownShot = 5f;
			set.range = 20;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.Crossbow);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.CrossbowBolt) };
			set.sleepSafetyPeriod = 1f;
			set.sleepType = EGuardSleepType.Day;
			set.typeXN = BuiltinBlocks.GuardCrossbowJobNightXN;
			set.typeXP = BuiltinBlocks.GuardCrossbowJobNightXP;
			set.typeZN = BuiltinBlocks.GuardCrossbowJobNightZN;
			set.typeZP = BuiltinBlocks.GuardCrossbowJobNightZP;
			return set;
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Night Crossbow Guard",
				maskColor1 = new Color32(195, 98, 27, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
