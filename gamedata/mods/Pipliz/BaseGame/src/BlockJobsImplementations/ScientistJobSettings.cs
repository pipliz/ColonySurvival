using BlockTypes;
using Jobs;
using NPC;
using Science;
using System.Collections.Generic;
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

			state.JobIsDone = true;

			// if no stored items - check if the stockpile contains the items required for any of the researches
			var completedCycles = scienceData.CompletedCycles;
			if (instance.StoredItemCount <= 0) {
				if (completedCycles.Count == 0) {
					// done for now
					state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(8f, 16f), BuiltinBlocks.Indices.erroridle));
					return;
				}

				int possibles = 0;
				for (int i = 0; i < completedCycles.Count; i++) {
					ScienceKey cyclesResearch = completedCycles.GetKeyAtIndex(i);
					if (cyclesResearch.Researchable.TryGetCyclesCondition(out ScientistCyclesCondition cyclesData) && completedCycles.GetValueAtIndex(i) < cyclesData.CycleCount) {
						possibles++;
						if (owner.Stockpile.Contains(cyclesData.ItemsPerCycle)) {
							instance.ShouldTakeItems = true;
							state.SetCooldown(0.3);
							return; // found an in-progress research that we have items for
						}
					}
				}

				// missing items, find random requirement to use as indicator
				if (possibles > 0) {
					int possiblesIdx = Random.Next(0, possibles);

					for (int i = 0; i < completedCycles.Count; i++) {
						ScienceKey cyclesResearch = completedCycles.GetKeyAtIndex(i);
						if (cyclesResearch.Researchable.TryGetCyclesCondition(out ScientistCyclesCondition cyclesData) && completedCycles.GetValueAtIndex(i) < cyclesData.CycleCount) {
							if (possiblesIdx-- <= 0) {
								MissingItemHelper(cyclesData.ItemsPerCycle, owner.Stockpile, ref state);
								return;
							}
						}
					}
					// should be unreachable
				}

				// done for now
				state.SetIndicator(new Shared.IndicatorState(Random.NextFloat(8f, 16f), BuiltinBlocks.Indices.erroridle));
				return;
			}

			// have stored items, try to forward an active science

			if (completedCycles.Count == 0) {
				instance.StoredItemCount = 0;
				state.SetCooldown(0.3);
				return;
			}

			for (int i = 0; i < completedCycles.Count; i++) {
				AbstractResearchable research = completedCycles.GetKeyAtIndex(i).Researchable;
				float progress = completedCycles.GetValueAtIndex(i);

				if (!research.TryGetCyclesCondition(out ScientistCyclesCondition cyclesCondition) || progress >= cyclesCondition.CycleCount) {
					continue;
				}

				float progressCycles = 1f;
				var happyData = owner.HappinessData;
				progressCycles *= happyData.ScienceSpeedMultiplierCalculator.GetSpeedMultiplier(happyData.CachedHappiness, instance.NPC);

				float nextProgress = progress + progressCycles;

				if ((int)nextProgress != (int)progress) {
					// euy reached next cycle. Better make sure we can get the requirements
					List<InventoryItem> requirements = cyclesCondition.ItemsPerCycle;

					if (owner.Stockpile.TryRemove(requirements)) {
						// recycle science bags
						int recycled = 0;
						for (int j = 0; j < requirements.Count; j++) {
							ushort type = requirements[j].Type;
							if (type == BuiltinBlocks.Indices.sciencebaglife
								|| type == BuiltinBlocks.Indices.sciencebagbasic
								|| type == BuiltinBlocks.Indices.sciencebagmilitary
							) {
								recycled += requirements[j].Amount;
							}
						}
						for (int j = recycled; j > 0; j--) {
							if (Random.NextDouble() > RECYCLE_CHANCE) {
								recycled--;
							}
						}
						if (recycled > 0) {
							owner.Stockpile.Add(BuiltinBlocks.Indices.linenbag, recycled);
						}
						OnSuccess(ref state);
						return;
					} else {
						continue;
					}
				} else {
					OnSuccess(ref state);
					return;
				}
				// unreachable
				void OnSuccess (ref NPCState stateCopy)
				{
					scienceData.CyclesAddProgress(research.AssignedKey, progressCycles);
					stateCopy.SetIndicator(new Shared.IndicatorState(CraftingCooldown, NPCIndicatorType.Science, (ushort)research.AssignedKey.Index));
					instance.StoredItemCount--;
				}
			}

			// had stored items, but for some reason couldn't dump them in existing research
			// reset the job basically
			instance.StoredItemCount = 0;
			state.SetCooldown(0.3);
			return;

			void MissingItemHelper (IList<InventoryItem> requirements, Stockpile stockpile, ref NPCState stateCopy)
			{
				ushort missing = 0;
				for (int i = 0; i < requirements.Count; i++) {
					if (!stockpile.Contains(requirements[i])) {
						missing = requirements[i].Type;
						break;
					}
				}
				float cooldown = Random.NextFloat(8f, 16f);
				stateCopy.SetIndicator(new Shared.IndicatorState(cooldown, missing, true, false));
				stateCopy.JobIsDone = false;
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
	}
}
