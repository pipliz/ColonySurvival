using NPC;
using Pipliz.JSON;
using Server.Monsters;
using System.Collections.Generic;
using UnityEngine;

namespace Pipliz.Mods.APIProvider.Jobs
{
	public class GuardBaseJob : BlockJobBase, IBlockJobBase
	{
		public IMonster target;
		public GuardSettings guardSettings;

		public virtual GuardSettings SetupSettings () { throw new System.NotImplementedException(); }

		public virtual void OnShoot ()
		{
			if (guardSettings.OnShootAudio != null) {
				ServerManager.SendAudio(position.Vector, guardSettings.OnShootAudio);
			}
			if (guardSettings.OnHitAudio != null) {
				ServerManager.SendAudio(target.PositionToAimFor, guardSettings.OnHitAudio);
			}

			{
				Vector3 start = position.Add(0, 1, 0).Vector;
				Vector3 end = target.PositionToAimFor;
				Vector3 dirNormalized = (end - start).normalized;
				ServerManager.SendParticleTrail(start + dirNormalized * 0.15f, end - dirNormalized * 0.15f, Random.NextFloat(1.5f, 2.5f));
			}

			target.OnHit(guardSettings.shootDamage, usedNPC, ModLoader.OnHitData.EHitSourceType.NPC);
		}

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

		public virtual bool HasTarget { get { return target != null && target.IsValid; } }

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			if (guardSettings == null) {
				guardSettings = SetupSettings();
			}
			return this;
		}

		public ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			InitializeJob(player, position, 0);
			if (guardSettings == null) {
				guardSettings = SetupSettings();
			}
			return this;
		}

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			if (HasTarget) {
				Vector3 npcPos = position.Add(0, 1, 0).Vector;
				Vector3 targetPos = target.PositionToAimFor;
				if (General.Physics.Physics.CanSee(npcPos, targetPos)) {
					usedNPC.LookAt(targetPos);
					ShootAtTarget(ref state); // <- sets cooldown
					return;
				} else {
					target = null;
				}
			}
			target = MonsterTracker.Find(position.Add(0, 1, 0), guardSettings.range, guardSettings.shootDamage);
			if (HasTarget) {
				usedNPC.LookAt(target.PositionToAimFor);
				ShootAtTarget(ref state); // <- sets cooldown
			} else {
				state.SetCooldown(guardSettings.cooldownSearchingTarget);
				if (worldTypeChecked) {
					Vector3 pos = usedNPC.Position.Vector;
					if (worldType == guardSettings.typeXP) {
						pos += Vector3.right;
					} else if (worldType == guardSettings.typeXN) {
						pos += Vector3.left;
					} else if (worldType == guardSettings.typeZP) {
						pos += Vector3.forward;
					} else if (worldType == guardSettings.typeZN) {
						pos += Vector3.back;
					}
					usedNPC.LookAt(pos);
				}
			}
		}

		protected override bool IsValidWorldType (ushort type)
		{
			if (guardSettings == null) { guardSettings = SetupSettings(); }
			return type == guardSettings.typeXP
				|| type == guardSettings.typeXN
				|| type == guardSettings.typeZP
				|| type == guardSettings.typeZN;
		}

		public virtual void ShootAtTarget (ref NPCBase.NPCState state)
		{
			if (Stockpile.GetStockPile(owner).TryRemove(guardSettings.shootItem)) {
				OnShoot();
				state.SetIndicator(new Shared.IndicatorState(guardSettings.cooldownShot, guardSettings.shootItem[0].Type));
			} else {
				state.SetIndicator(new Shared.IndicatorState(guardSettings.cooldownMissingItem, guardSettings.shootItem[0].Type, true, false));
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
