using NPC;
using System.Collections.Generic;
using UnityEngine;
using static NPC.NPCBase;

namespace Pipliz.APIProvider.Jobs
{
	public class GuardJobSettings : IBlockJobSettings
	{
		public virtual ItemTypes.ItemType[] BlockTypes { get; set; }
		public virtual NPCType NPCType { get; set; }
		public virtual string NPCTypeKey { get; set; }
		public virtual string OnShootAudio { get; set; }
		public virtual InventoryItem RecruitmentItem { get; set; }
		public virtual List<InventoryItem> ShootItem { get; set; }
		public virtual EGuardSleepType SleepType { get; set; }
		public virtual int Range { get; set; }
		public virtual float Damage { get; set; }
		public virtual float CooldownShot { get; set; }
		public virtual ItemTypes.ItemTypeDrops OnShootResultItem { get; set; }

		public virtual float CooldownSearchingTarget { get; set; }
		public virtual float CooldownMissingItem { get; set; }
		public virtual float SleepSafetyPeriod { get; set; }
		public virtual string OnHitAudio { get; set; }

		public virtual bool ToSleep
		{
			get
			{
				switch (SleepType) {
					case EGuardSleepType.Day:
					default:
						return TimeCycle.TimeOfDay >= 10f && TimeCycle.TimeOfDay < TimeCycle.SunSet - SleepSafetyPeriod;
					case EGuardSleepType.Night:
						return !TimeCycle.IsDay && TimeCycle.HoursSinceSunSet > SleepSafetyPeriod;
				}
			}
		}

		public GuardJobSettings () { }

		public GuardJobSettings (
			string blockTypeKey,
			string npcTypeKey,
			EGuardSleepType sleepType,
			float damage,
			int range,
			float cooldownShot,
			string shootAudio,
			InventoryItem shootItem,
			InventoryItem recruitmentItem
		)
		{
			BlockTypes = new ItemTypes.ItemType[] {
				ItemTypes.GetType(blockTypeKey),
				ItemTypes.GetType(blockTypeKey + "x+"),
				ItemTypes.GetType(blockTypeKey + "x-"),
				ItemTypes.GetType(blockTypeKey + "z+"),
				ItemTypes.GetType(blockTypeKey + "z-")
			};

			OnShootAudio = shootAudio;
			ShootItem = new List<InventoryItem>() { shootItem };
			NPCTypeKey = npcTypeKey;
			NPCType = NPCType.GetByKeyNameOrDefault(npcTypeKey);
			SleepType = sleepType;
			Damage = damage;
			Range = range;
			RecruitmentItem = recruitmentItem;
			CooldownShot = cooldownShot;

			CooldownSearchingTarget = 0.5f;
			CooldownMissingItem = 1.5f;
			OnHitAudio = "fleshHit";
		}

		public Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			return instance.Position;
		}

		public void OnNPCAtJob (BlockJobInstance blockInstance, ref NPCState state)
		{
			GuardJobInstance instance = (GuardJobInstance)blockInstance;
			Vector3Int position = instance.Position;
			if (instance.HasTarget) {
				Vector3 npcPos = position.Add(0, 1, 0).Vector;
				Vector3 targetPos = instance.Target.PositionToAimFor;
				if (VoxelPhysics.CanSee(npcPos, targetPos)) {
					instance.NPC.LookAt(targetPos);
					ShootAtTarget(instance, ref state); // <- sets cooldown
					return;
				}
			}
			instance.Target = MonsterTracker.Find(position.Add(0, 1, 0), Range, Damage);
			if (instance.HasTarget) {
				instance.NPC.LookAt(instance.Target.PositionToAimFor);
				ShootAtTarget(instance, ref state); // <- sets cooldown
			} else {
				state.SetCooldown(CooldownSearchingTarget);
				Vector3 pos = instance.NPC.Position.Vector;
				if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
					switch (index) {
						case 1:
							pos.x += 1f;
							break;
						case 2:
							pos.x -= 1f;
							break;
						case 3:
							pos.z += 1f;
							break;
						case 4:
							pos.z -= 1f;
							break;
					}
				}
				instance.NPC.LookAt(pos);
			}
		}

		public void ShootAtTarget (GuardJobInstance instance, ref NPCState state)
		{
			if (instance.Owner.Stockpile.TryRemove(ShootItem)) {
				if (OnShootAudio != null) {
					ServerManager.SendAudio(instance.Position.Vector, OnShootAudio);
				}
				if (OnHitAudio != null) {
					ServerManager.SendAudio(instance.Target.PositionToAimFor, OnHitAudio);
				}

				{
					Vector3 start = instance.Position.Add(0, 1, 0).Vector;
					Vector3 end = instance.Target.PositionToAimFor;
					Vector3 dirNormalized = (end - start).normalized;
					ServerManager.SendParticleTrail(start + dirNormalized * 0.15f, end - dirNormalized * 0.15f, Random.NextFloat(1.5f, 2.5f));
				}

				instance.Target.OnHit(Damage, instance.NPC, ModLoader.OnHitData.EHitSourceType.NPC);
				state.SetIndicator(new Shared.IndicatorState(CooldownShot, ShootItem[0].Type));

				if (OnShootResultItem.item.Type > 0) {
					if (Random.NextDouble(0.0, 1.0) <= OnShootResultItem.chance) {
						instance.Owner.Stockpile.Add(OnShootResultItem.item);
					}
				}
			} else {
				state.SetIndicator(new Shared.IndicatorState(CooldownMissingItem, ShootItem[0].Type, true, false));
			}
		}

		public void OnNPCAtStockpile (BlockJobInstance instance, ref NPCState state)
		{
			// nothing to do at stockpile (doesn't take/get items at all - magically teleports them when needed)
		}

		public void OnGoalChanged (BlockJobInstance instance, NPCGoal goalOld, NPCGoal goalNew)
		{
			// doesn't need to do anything like resetting block state upon leaving
		}

		public enum EGuardSleepType : byte
		{
			Day,
			Night
		}
	}
}
