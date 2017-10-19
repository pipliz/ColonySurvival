using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardCrossbowJobDay : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardcrossbowday"; } }

		protected override GuardSettings SetupSettings ()
		{
			GuardSettings set = new GuardSettings();
			set.cooldownMissingItem = 1.5f;
			set.cooldownSearchingTarget = 0.5f;
			set.cooldownShot = 5f;
			set.range = 25;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.Crossbow);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.CrossbowBolt) };
			set.shootDamage = 40f;
			set.sleepSafetyPeriod = 1f;
			set.sleepType = EGuardSleepType.Night;
			set.typeXN = BuiltinBlocks.GuardCrossbowJobDayXN;
			set.typeXP = BuiltinBlocks.GuardCrossbowJobDayXP;
			set.typeZN = BuiltinBlocks.GuardCrossbowJobDayZN;
			set.typeZP = BuiltinBlocks.GuardCrossbowJobDayZP;
			return set;
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Day Crossbow Guard",
				maskColor1 = new Color32(195, 98, 27, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
