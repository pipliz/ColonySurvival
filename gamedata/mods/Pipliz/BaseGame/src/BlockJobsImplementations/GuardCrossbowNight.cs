using BlockTypes.Builtin;
using Pipliz.Mods.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.Mods.BaseGame.BlockNPCs
{
	public class GuardCrossbowJobNight : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardcrossbownight"; } }

		public static GuardSettings CachedSettings;

		public static GuardSettings GetGuardSettings ()
		{
			if (CachedSettings == null) {
				GuardSettings set = new GuardSettings();
				set.cooldownMissingItem = 1.5f;
				set.cooldownSearchingTarget = 0.5f;
				set.cooldownShot = 8f;
				set.range = 25;
				set.recruitmentItem = new InventoryItem(BuiltinBlocks.Crossbow);
				set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.CrossbowBolt) };
				set.shootDamage = 300f;
				set.sleepSafetyPeriod = 1f;
				set.sleepType = EGuardSleepType.Day;
				set.typeXN = BuiltinBlocks.GuardCrossbowJobNightXN;
				set.typeXP = BuiltinBlocks.GuardCrossbowJobNightXP;
				set.typeZN = BuiltinBlocks.GuardCrossbowJobNightZN;
				set.typeZP = BuiltinBlocks.GuardCrossbowJobNightZP;
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
				printName = "Night Crossbow Guard",
				maskColor1 = new Color32(52, 52, 52, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
