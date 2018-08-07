using BlockTypes;
using NPC;
using Pipliz.APIProvider.Jobs;
using Science;
using System.Collections.Generic;
using UnityEngine.Assertions;
using static NPC.NPCBase;

namespace Pipliz.Mods.BaseGame
{
	public class ScientistJobSettings : IBlockJobSettings
	{
		public virtual ItemTypes.ItemType[] BlockTypes { get; set; }
		public virtual NPCType NPCType { get; set; }
		public virtual float CraftingCooldown { get; set; }

		static double RECYCLE_CHANCE = 0.8;

		public virtual string NPCTypeKey { get { return "pipliz.scientist"; } }
		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }
		public InventoryItem RecruitmentItem { get { return InventoryItem.Empty; } }

		// buffer for crafting results - not threadsafe but hey npc's aren't threaded
		static protected List<InventoryItem> craftingResults = new List<InventoryItem>();

		public ScientistJobSettings ()
		{
			BlockTypes = new ItemTypes.ItemType[] { ItemTypes.GetType("sciencelab") };
			NPCType = NPCType.GetByKeyNameOrDefault(NPCTypeKey);
			CraftingCooldown = 14f;
		}

		public virtual void OnGoalChanged (BlockJobInstance instance, NPCGoal oldGoal, NPCGoal newGoal) { }

		public virtual Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			return instance.Position;
		}

		public virtual void OnNPCAtJob (BlockJobInstance instance, ref NPCState state)
		{
			instance.NPC.LookAt(instance.Position.Vector);
			ColonyScienceState scienceData = instance.Owner.ScienceData;
			ScienceKey activeResearch = scienceData.ActiveResearch;
			if (state.Inventory.IsEmpty) {
				if (activeResearch.IsValid && !activeResearch.IsCompleted(scienceData)) {
					// no items, but valid research -> try to get items
					IList<InventoryItem> requirements = activeResearch.Researchable.Researchable.GetScienceRequirements();
					if (instance.Owner.Stockpile.Contains(requirements)) {
						instance.ShouldTakeItems = true;
						state.SetCooldown(0.3);
						state.JobIsDone = true;
					} else {
						ushort missing = 0;
						for (int i = 0; i < requirements.Count; i++) {
							if (!instance.Owner.Stockpile.Contains(requirements[i])) {
								missing = requirements[i].Type;
								break;
							}
						}
						float cooldown = Random.NextFloat(8f, 16f);
						state.SetIndicator(new Shared.IndicatorState(cooldown, missing, true, false));
						state.JobIsDone = false;
					}
				} else {
					float cooldown = Random.NextFloat(8f, 16f);
					// no items, no research -> wait for research
					state.SetIndicator(new Shared.IndicatorState(cooldown, BuiltinBlocks.ErrorIdle));
					state.JobIsDone = false;
				}
			} else {
				if (activeResearch.IsValid && activeResearch.IsCompleted(scienceData)) {
					// items, but no valid research -> go dump items
					instance.ShouldTakeItems = true;
					state.SetCooldown(0.3);
					state.JobIsDone = true;
				} else {
					// items, valid research -> try progress
					IList<InventoryItem> requirements = activeResearch.Researchable.Researchable.GetScienceRequirements();
					if (state.Inventory.TryRemove(requirements)) {
						int recycled = 0;
						for (int i = 0; i < requirements.Count; i++) {
							ushort type = requirements[i].Type;
							if (type == BuiltinBlocks.ScienceBagLife
								|| type == BuiltinBlocks.ScienceBagBasic
								|| type == BuiltinBlocks.ScienceBagMilitary) {
								recycled += requirements[i].Amount;
							}
						}
						for (int i = recycled; i > 0; i--) {
							if (Random.NextDouble() > RECYCLE_CHANCE) {
								recycled--;
							}
						}
						state.Inventory.Add(BuiltinBlocks.LinenBag, recycled);
						scienceData.AddActiveResearchProgress(1);
						state.SetIndicator(new Shared.IndicatorState(CraftingCooldown, NPCIndicatorType.Science));
					} else {
						state.SetCooldown(0.3);
					}
					instance.ShouldTakeItems = true;
					state.JobIsDone = true;
				}
			}
		}

		public virtual void OnNPCAtStockpile (BlockJobInstance instance, ref NPCState state)
		{
			if (state.Inventory.IsEmpty) {
				Assert.IsTrue(instance.ShouldTakeItems);
			} else {
				state.Inventory.Dump(instance.NPC.Colony.Stockpile);
			}
			state.SetCooldown(0.5);
			state.JobIsDone = true;
			if (instance.ShouldTakeItems) {
				instance.ShouldTakeItems = false;

				ColonyScienceState science = instance.Owner.ScienceData;
				if (!science.ActiveResearch.IsCompleted(science)) {
					var requirements = science.ActiveResearch.Researchable.Researchable.GetScienceRequirements();
					if (instance.Owner.Stockpile.TryRemove(requirements)) {
						state.Inventory.Add(requirements);
					}
				}
			}
		}
	}
}
