using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class GuardSlingerJobDay : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardslingerday"; } }

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
				set.sleepType = EGuardSleepType.Night;
				set.typeXN = BuiltinBlocks.GuardSlingerJobDayXN;
				set.typeXP = BuiltinBlocks.GuardSlingerJobDayXP;
				set.typeZN = BuiltinBlocks.GuardSlingerJobDayZN;
				set.typeZP = BuiltinBlocks.GuardSlingerJobDayZP;
				set.OnShootAudio = "sling";
				set.OnHitAudio = "fleshHit";
				CachedSettings = set;
			}
			return CachedSettings;
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
				printName = "Day Slinger Guard",
				maskColor1 = new Color32(136, 136, 136, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
