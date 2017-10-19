using BlockTypes.Builtin;
using Pipliz.APIProvider.Jobs;
using Server.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.BlockNPCs.Implementations
{
	public class GuardSlingerJobDay : GuardBaseJob, INPCTypeDefiner
	{
		public override string NPCTypeKey { get { return "pipliz.guardslingerday"; } }

		protected override GuardSettings SetupSettings ()
		{
			GuardSettings set = new GuardSettings();
			set.cooldownMissingItem = 1.5f;
			set.cooldownSearchingTarget = 0.5f;
			set.cooldownShot = 1.5f;
			set.range = 15;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.Sling);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.SlingBullet) };
			set.shootDamage = 10f;
			set.sleepSafetyPeriod = 1f;
			set.sleepType = EGuardSleepType.Night;
			set.typeXN = BuiltinBlocks.GuardSlingerJobDayXN;
			set.typeXP = BuiltinBlocks.GuardSlingerJobDayXP;
			set.typeZN = BuiltinBlocks.GuardSlingerJobDayZN;
			set.typeZP = BuiltinBlocks.GuardSlingerJobDayZP;
			return set;
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
