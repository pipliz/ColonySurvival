using BlockTypes;
using Jobs;
using NPC;
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
		public virtual int ItemsToTakePerHaul { get; set; } = 5;

		static double RECYCLE_CHANCE = 0.8;

		public virtual string NPCTypeKey { get { return "pipliz.scientist"; } }
		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }
		public InventoryItem RecruitmentItem { get { return InventoryItem.Empty; } }
		public float NPCShopGameHourMinimum { get { return TimeCycle.Settings.SleepTimeEnd; } }
		public float NPCShopGameHourMaximum { get { return TimeCycle.Settings.SleepTimeStart; } }

		public ScientistJobSettings ()
		{
			BlockTypes = new ItemTypes.ItemType[] { BuiltinBlocks.Types.sciencelab };
			NPCType = NPCType.GetByKeyNameOrDefault(NPCTypeKey);
			CraftingCooldown = 14f;
		}

		public virtual void OnGoalChanged (BlockJobInstance instance, NPCGoal oldGoal, NPCGoal newGoal) { }

		public virtual Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			return instance.Position;
		}

		public virtual void OnNPCAtJob (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			ScientistJobInstance instance = blockJobInstance as ScientistJobInstance;
			if (instance == null) {
				state.JobIsDone = true;
				return;
			}

			Colony owner = instance.Owner;
			instance.NPC.LookAt(instance.Position.Vector);
			ColonyScienceState scienceData = owner.ScienceData;
			ScienceKey activeResearch = scienceData.ActiveResearch;
			if (instance.StoredItemCount <= 0) {
				if (activeResearch.IsValid && !activeResearch.IsCompleted(scienceData)) {
					// no items, but valid research -> try to get items
					IList<InventoryItem> requirements = activeResearch.Researchable.GetScienceRequirements();
					if (owner.Stockpile.Contains(requirements)) {
						instance.ShouldTakeItems = true;
						state.SetCooldown(0.3);
						state.JobIsDone = true;
					} else {
						MissingItemHelper(requirements, owner.Stockpile, ref state);
					}
				} else {
					float cooldown = Random.NextFloat(8f, 16f);
					// no items, no research -> wait for research
					state.SetIndicator(new Shared.IndicatorState(cooldown, BuiltinBlocks.Indices.erroridle));
					state.JobIsDone = false;
				}
			} else {
				state.JobIsDone = true;

				if (!activeResearch.IsValid) {
					// items, but no valid research -> 'dump' items
					instance.StoredItemCount = 0;
					state.SetCooldown(0.3);
					return;
				}
				IResearchable research = activeResearch.Researchable;
				float progress = activeResearch.GetProgress(scienceData);
				if (progress >= research.GetResearchIterationCount()) {
					activeResearch = new ScienceKey();
					instance.StoredItemCount = 0;
					state.SetCooldown(0.3);
					return;
				}

				// items, valid research -> try progress

				float progressCycles = 1f;
				var happyData = owner.HappinessData;
				progressCycles *= happyData.ScienceSpeedMultiplierCalculator.GetSpeedMultiplier(happyData.CachedHappiness, instance.NPC);

				float nextProgress = progress + progressCycles;

				if ((int)nextProgress != (int)progress) {
					// euy reached next cycle. Better make sure we can get the requirements
					IList<InventoryItem> requirements = research.GetScienceRequirements();

					if (owner.Stockpile.TryRemove(requirements)) {
						// recycle science bags
						int recycled = 0;
						for (int i = 0; i < requirements.Count; i++) {
							ushort type = requirements[i].Type;
							if (   type == BuiltinBlocks.Indices.sciencebaglife
								|| type == BuiltinBlocks.Indices.sciencebagbasic
								|| type == BuiltinBlocks.Indices.sciencebagmilitary
							) {
								recycled += requirements[i].Amount;
							}
						}
						for (int i = recycled; i > 0; i--) {
							if (Random.NextDouble() > RECYCLE_CHANCE) {
								recycled--;
							}
						}
						owner.Stockpile.Add(BuiltinBlocks.Indices.linenbag, recycled);

						OnSuccess(instance, scienceData, progressCycles, ref state);
					} else {
						MissingItemHelper(requirements, owner.Stockpile, ref state);
					}
				} else {
					OnSuccess(instance, scienceData, progressCycles, ref state);
				}
			}
		}

		public virtual void OnNPCAtStockpile (BlockJobInstance instance, ref NPCState state)
		{
			ScientistJobInstance scientist = instance as ScientistJobInstance;
			if (scientist != null) {
				scientist.StoredItemCount = ItemsToTakePerHaul;
			}
			state.SetCooldown(0.5);
			state.JobIsDone = true;
			instance.ShouldTakeItems = false;
		}

		protected void OnSuccess (ScientistJobInstance instance, ColonyScienceState scienceData, float progressCycles, ref NPCState state)
		{
			scienceData.AddActiveResearchProgress(progressCycles);
			state.SetIndicator(new Shared.IndicatorState(CraftingCooldown, NPCIndicatorType.Science));
			instance.StoredItemCount--;
		}

		protected static void MissingItemHelper (IList<InventoryItem> requirements, Stockpile stockpile, ref NPCState state)
		{
			ushort missing = 0;
			for (int i = 0; i < requirements.Count; i++) {
				if (!stockpile.Contains(requirements[i])) {
					missing = requirements[i].Type;
					break;
				}
			}
			float cooldown = Random.NextFloat(8f, 16f);
			state.SetIndicator(new Shared.IndicatorState(cooldown, missing, true, false));
			state.JobIsDone = false;
		}
	}
}
