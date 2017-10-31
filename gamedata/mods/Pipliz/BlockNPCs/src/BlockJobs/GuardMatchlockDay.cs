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

		public static GuardSettings CachedSettings;

		public static GuardSettings GetGuardSettings ()
		{
			if (CachedSettings == null) {
				GuardSettings set = new GuardSettings();
				set.cooldownMissingItem = 1.5f;
				set.cooldownSearchingTarget = 0.5f;
				set.cooldownShot = 12f;
				set.range = 30;
				set.recruitmentItem = new InventoryItem(BuiltinBlocks.MatchlockGun);
				set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.LeadBullet), new InventoryItem(BuiltinBlocks.GunpowderPouch) };
				set.shootDamage = 500f;
				set.sleepSafetyPeriod = 1f;
				set.sleepType = EGuardSleepType.Night;
				set.typeXN = BuiltinBlocks.GuardMatchlockJobDayXN;
				set.typeXP = BuiltinBlocks.GuardMatchlockJobDayXP;
				set.typeZN = BuiltinBlocks.GuardMatchlockJobDayZN;
				set.typeZP = BuiltinBlocks.GuardMatchlockJobDayZP;
				set.OnShootAudio = "matchlock";
				set.OnHitAudio = "fleshHit";
				CachedSettings = set;
			}
			return CachedSettings;
		}

		protected override void OnShoot ()
		{
			if (Random.NextFloat(0f, 1f) < 0.9f) {
				Stockpile.GetStockPile(owner).Add(BuiltinBlocks.LinenPouch);
			}
			base.OnShoot();
		}

		protected override GuardSettings SetupSettings ()
		{
			return GetGuardSettings();
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
