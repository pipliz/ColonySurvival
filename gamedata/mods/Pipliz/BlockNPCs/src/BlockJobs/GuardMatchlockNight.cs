using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardMatchlockJobNight : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardmatchlocknight"; } }

		protected override GuardSettings SetupSettings ()
		{
			GuardSettings set = new GuardSettings();
			set.cooldownMissingItem = 1.5f;
			set.cooldownSearchingTarget = 0.5f;
			set.cooldownShot = 10f;
			set.range = 30;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.MatchlockGun);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.LeadBullet), new InventoryItem(BuiltinBlocks.GunpowderPouch) };
			set.shootDamage = 100f;
			set.sleepSafetyPeriod = 1f;
			set.sleepType = EGuardSleepType.Day;
			set.typeXN = BuiltinBlocks.GuardMatchlockJobNightXN;
			set.typeXP = BuiltinBlocks.GuardMatchlockJobNightXP;
			set.typeZN = BuiltinBlocks.GuardMatchlockJobNightZN;
			set.typeZP = BuiltinBlocks.GuardMatchlockJobNightZP;
			return set;
		}

		NPCTypeStandardSettings INPCTypeDefiner.GetNPCTypeDefinition ()
		{
			return new NPCTypeStandardSettings()
			{
				keyName = NPCTypeKey,
				printName = "Night Matchlock Guard",
				maskColor1 = new Color32(205, 207, 141, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
