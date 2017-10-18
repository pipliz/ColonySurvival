using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardMatchlockJobDay : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardmatchlockday"; } }

		protected override GuardSettings SetupSettings ()
		{
			GuardSettings set = new GuardSettings();
			set.cooldownMissingItem = 1.5f;
			set.cooldownSearchingTarget = 0.3f;
			set.cooldownShot = 5f;
			set.range = 20;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.MatchlockGun);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.LeadBullet), new InventoryItem(BuiltinBlocks.GunpowderPouch) };
			set.sleepSafetyPeriod = 1f;
			set.sleepType = EGuardSleepType.Night;
			set.typeXN = BuiltinBlocks.GuardMatchlockJobDayXN;
			set.typeXP = BuiltinBlocks.GuardMatchlockJobDayXP;
			set.typeZN = BuiltinBlocks.GuardMatchlockJobDayZN;
			set.typeZP = BuiltinBlocks.GuardMatchlockJobDayZP;
			return set;
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Day Matchlock Guard",
				maskColor1 = new Color32(205, 207, 141, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
