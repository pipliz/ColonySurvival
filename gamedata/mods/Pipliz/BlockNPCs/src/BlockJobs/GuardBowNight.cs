﻿using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardBowJobNight : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardbownight"; } }

		public static GuardSettings CachedSettings;

		public static GuardSettings GetGuardSettings ()
		{
			if (CachedSettings == null) {
				GuardSettings set = new GuardSettings();
				set.cooldownMissingItem = 1.5f;
				set.cooldownSearchingTarget = 0.5f;
				set.cooldownShot = 5f;
				set.range = 20;
				set.recruitmentItem = new InventoryItem(BuiltinBlocks.Bow);
				set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.BronzeArrow) };
				set.shootDamage = 100f;
				set.sleepSafetyPeriod = 1f;
				set.sleepType = EGuardSleepType.Day;
				set.typeXN = BuiltinBlocks.GuardBowJobNightXN;
				set.typeXP = BuiltinBlocks.GuardBowJobNightXP;
				set.typeZN = BuiltinBlocks.GuardBowJobNightZN;
				set.typeZP = BuiltinBlocks.GuardBowJobNightZP;
				set.OnShootAudio = "bowShoot";
				set.OnHitAudio = "fleshHit";
				CachedSettings = set;
			}
			return CachedSettings;
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
				printName = "Night Bow Guard",
				maskColor1 = new Color32(160, 107, 50, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
