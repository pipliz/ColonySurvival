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
			set.cooldownSearchingTarget = 0.3f;
			set.cooldownShot = 5f;
			set.range = 20;
			set.recruitmentItem = new InventoryItem(BuiltinBlocks.Sling);
			set.shootItem = new List<InventoryItem>() { new InventoryItem(BuiltinBlocks.SlingBullet) };
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
				maskColor1 = new Color32(255, 0, 255, 255),
				type = NPCTypeID.GetNextID()
			};
		}
	}
}
