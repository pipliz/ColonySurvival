using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardSlingerJobNight : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardslingernight"; } }

		public static GuardSettings CachedSettings;

		public static GuardSettings GetGuardSettings ()
		{
			if (CachedSettings == null) {
				GuardSettings set = new GuardSettings();
				set.cooldownMissingItem = 1.5f;
				set.cooldownSearchingTarget = 0.5f;
				set.cooldownShot = 3f;
				set.range = 12;
				set.recruitmentItem = new InventoryItem(BuiltinBlocks.Sling);
				set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.SlingBullet) };
				set.shootDamage = 50f;
				set.sleepSafetyPeriod = 1f;
				set.sleepType = EGuardSleepType.Day;
				set.typeXN = BuiltinBlocks.GuardSlingerJobNightXN;
				set.typeXP = BuiltinBlocks.GuardSlingerJobNightXP;
				set.typeZN = BuiltinBlocks.GuardSlingerJobNightZN;
				set.typeZP = BuiltinBlocks.GuardSlingerJobNightZP;
				set.OnShootAudio = "sling";
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
				printName = "Night Slinger Guard",
				maskColor1 = new Color32(136, 136, 136, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
