using NPC;
using Pipliz.JSON;
using Server.Monsters;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.APIProvider.Jobs
{
	public class GuardBaseJob : BlockJobBase, IBlockJobBase
	{
		protected ushort blockType;
		public IMonster target;
		public GuardSettings guardSettings;

		protected virtual GuardSettings SetupSettings () { throw new System.NotImplementedException(); }

		protected virtual void OnShoot ()
		{
			if (guardSettings.OnShootAudio != null) {
				ServerManager.SendAudio(position.Vector, guardSettings.OnShootAudio);
			}
			if (guardSettings.OnHitAudio != null) {
				ServerManager.SendAudio(target.PositionToAimFor, guardSettings.OnHitAudio);
			}
			target.OnHit(guardSettings.shootDamage);
		}

		public override float TimeBetweenJobs { get { return 2.5f; } }

		public override bool ToSleep
		{
			get
			{
				switch (guardSettings.sleepType) {
					case EGuardSleepType.Day:
					default:
						return TimeCycle.TimeOfDay >= 10f && TimeCycle.TimeOfDay < TimeCycle.SunSet - guardSettings.sleepSafetyPeriod;
					case EGuardSleepType.Night:
						return !TimeCycle.IsDay && TimeCycle.HoursSinceSunSet > guardSettings.sleepSafetyPeriod;
				}
			}
		}

		public override InventoryItem RecruitementItem { get { return guardSettings.recruitmentItem; } }

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			blockType = ItemTypes.IndexLookup.GetIndex(node.GetAs<string>("type"));
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			if (guardSettings == null) {
				guardSettings = SetupSettings();
			}
			return this;
		}

		public ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			blockType = type;
			InitializeJob(player, position, 0);
			if (guardSettings == null) {
				guardSettings = SetupSettings();
			}
			return this;
		}

		public override JSONNode GetJSON ()
		{
			return base.GetJSON()
				.SetAs("type", ItemTypes.IndexLookup.GetName(blockType));
		}

		public override void OnNPCDoJob (ref NPCBase.NPCState state)
		{
			if (target != null && target.IsValid) {
				Vector3 npcPos = usedNPC.Position + Vector3.up;
				Vector3 targetPos = target.Position + Vector3.up;
				if (General.Physics.Physics.CanSee(npcPos, targetPos)) {
					usedNPC.LookAt(targetPos);
					if (Stockpile.GetStockPile(owner).TryRemove(guardSettings.shootItem)) {
						OnShoot();
						state.SetIndicator(NPCIndicatorType.Crafted, guardSettings.cooldownShot, guardSettings.shootItem[0].Type);
						OverrideCooldown(guardSettings.cooldownShot);
					} else {
						state.SetIndicator(NPCIndicatorType.MissingItem, guardSettings.cooldownMissingItem, guardSettings.shootItem[0].Type);
						OverrideCooldown(guardSettings.cooldownMissingItem);
					}
				} else {
					target = null;
				}
			}
			if (target == null || !target.IsValid) {
				target = MonsterTracker.Find(position.Add(0, 1, 0), guardSettings.range);
				if (target == null) {
					Vector3 pos = usedNPC.Position;
					if (blockType == guardSettings.typeXP) {
						pos += Vector3.right;
					} else if (blockType == guardSettings.typeXN) {
						pos += Vector3.left;
					} else if (blockType == guardSettings.typeZP) {
						pos += Vector3.forward;
					} else if (blockType == guardSettings.typeZN) {
						pos += Vector3.back;
					}
					usedNPC.LookAt(pos);
				} else {
					OverrideCooldown(guardSettings.cooldownSearchingTarget);
				}
			}
		}

		public class GuardSettings
		{
			public int range;
			public float cooldownShot;
			public float cooldownMissingItem;
			public float cooldownSearchingTarget;
			public float sleepSafetyPeriod;
			public InventoryItem recruitmentItem;
			public IList<InventoryItem> shootItem;
			public float shootDamage;
			public EGuardSleepType sleepType;
			public ushort typeXP;
			public ushort typeXN;
			public ushort typeZP;
			public ushort typeZN;
			public string OnShootAudio;
			public string OnHitAudio;
		}

		public enum EGuardSleepType : byte
		{
			Day,
			Night
		}
	}
}
