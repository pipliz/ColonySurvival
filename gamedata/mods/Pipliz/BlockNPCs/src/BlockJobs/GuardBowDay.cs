using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardBowJobDay : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardbowday"; } }

		protected override GuardSettings SetupSettings ()
		{
			GuardSettings set = new GuardSettings();
			set.cooldownMissingItem = 1.5f;
			set.cooldownSearchingTarget = 0.5f;
			set.cooldownShot = 3f;
			set.range = 20;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.Bow);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.BronzeArrow) };
			set.shootDamage = 20f;
			set.sleepSafetyPeriod = 1f;
			set.sleepType = EGuardSleepType.Night;
			set.typeXN = BuiltinBlocks.GuardBowJobDayXN;
			set.typeXP = BuiltinBlocks.GuardBowJobDayXP;
			set.typeZN = BuiltinBlocks.GuardBowJobDayZN;
			set.typeZP = BuiltinBlocks.GuardBowJobDayZP;
			return set;
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Day Bow Guard",
				maskColor1 = new Color32(160, 107, 50, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
