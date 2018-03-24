using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class GuardMatchlockJobNight : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardmatchlocknight"; } }

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
				set.sleepType = EGuardSleepType.Day;
				set.typeXN = BuiltinBlocks.GuardMatchlockJobNightXN;
				set.typeXP = BuiltinBlocks.GuardMatchlockJobNightXP;
				set.typeZN = BuiltinBlocks.GuardMatchlockJobNightZN;
				set.typeZP = BuiltinBlocks.GuardMatchlockJobNightZP;
				set.OnShootAudio = "matchlock";
				set.OnHitAudio = "fleshHit";
				CachedSettings = set;
			}
			return CachedSettings;
		}

		public override void OnShoot ()
		{
			if (Random.NextFloat(0f, 1f) < 0.9f) {
				Stockpile.GetStockPile(owner).Add(BuiltinBlocks.LinenPouch);
			}
			base.OnShoot();
		}

		public override GuardSettings SetupSettings ()
		{
			return GetGuardSettings();
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
