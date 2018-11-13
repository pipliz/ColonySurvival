using BlockTypes;
using Jobs;
using NPC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static NPC.NPCBase;

namespace Pipliz.Mods.BaseGame.Construction
{
	public class ConstructionJobSettings : IBlockJobSettings
	{
		public virtual ItemTypes.ItemType[] BlockTypes { get; set; }
		public virtual NPCType NPCType { get; set; }
		public virtual string NPCTypeKey { get; set; }
		public virtual InventoryItem RecruitmentItem { get; set; }
		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }
		public virtual int ItemsFetchedAtStockpileCount { get; set; }
		public float NPCShopGameHourMinimum { get { return TimeCycle.Settings.SleepTimeEnd; } }
		public float NPCShopGameHourMaximum { get { return TimeCycle.Settings.SleepTimeStart; } }

		// buffer for onnpcgathered npc code should be threadsafe
		static protected List<ItemTypes.ItemTypeDrops> GatherResults = new List<ItemTypes.ItemTypeDrops>();

		public ConstructionJobSettings (int fetchItemsCount = 5)
		{
			ItemsFetchedAtStockpileCount = fetchItemsCount;
			BlockTypes = new ItemTypes.ItemType[] {
				ItemTypes.GetType("constructionjob"),
				ItemTypes.GetType("constructionjobx-"),
				ItemTypes.GetType("constructionjobx+"),
				ItemTypes.GetType("constructionjobz-"),
				ItemTypes.GetType("constructionjobz+")
			};
			NPCTypeKey = "pipliz.constructor";
			NPCType = NPCType.GetByKeyNameOrDefault(NPCTypeKey);
		}

		public virtual void OnGoalChanged (BlockJobInstance instance, NPCGoal oldGoal, NPCGoal newGoal) { }

		public virtual Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			return instance.Position;
		}

		public virtual void OnNPCAtJob (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			ConstructionJobInstance instance = (ConstructionJobInstance)blockJobInstance;

			if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
				Vector3 rotate = instance.NPC.Position.Vector;
				switch (index) {
					case 1:
						rotate.x -= 1f;
						break;
					case 2:
						rotate.x += 1f;
						break;
					case 3:
						rotate.z -= 1f;
						break;
					case 4:
						rotate.z += 1f;
						break;
				}
				instance.NPC.LookAt(rotate);
			}
			if (instance.ConstructionArea != null && !instance.ConstructionArea.IsValid) {
				instance.ConstructionArea = null;
			}

			if (instance.ConstructionArea == null) {
				if (AreaJobTracker.ExistingAreaAt(instance.Position.Add(-1, -1, -1), instance.Position.Add(1, 1, 1), out List<IAreaJob> jobs)) {
					for (int i = 0; i < jobs.Count; i++) {
						if (jobs[i] is ConstructionArea neighbourArea) {
							instance.ConstructionArea = neighbourArea;
							break;
						}
					}
					AreaJobTracker.AreaJobListPool.Return(jobs);
				}

				if (instance.ConstructionArea == null) {
					if (instance.DidAreaPresenceTest) {
						state.SetCooldown(0.5);
						ServerManager.TryChangeBlock(instance.Position, null, ItemTypes.Air, instance.Owner);
					} else {
						state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(3f, 5f), BuiltinBlocks.ErrorIdle));
						instance.DidAreaPresenceTest = true;
					}
					return;
				}
			}

			Assert.IsNotNull(instance.ConstructionArea);
			instance.ConstructionArea.DoJob(instance, ref state);
		}

		public virtual void OnNPCAtStockpile (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			((ConstructionJobInstance)blockJobInstance).StoredItemCount = ItemsFetchedAtStockpileCount;
			state.Inventory.Dump(blockJobInstance.Owner.Stockpile);
			state.SetCooldown(0.3);
			state.JobIsDone = true;
			blockJobInstance.ShouldTakeItems = false;
		}
	}
}
