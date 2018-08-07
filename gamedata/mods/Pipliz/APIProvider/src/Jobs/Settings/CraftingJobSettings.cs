using BlockTypes;
using NPC;
using Recipes;
using System.Collections.Generic;
using UnityEngine.Assertions;
using static NPC.NPCBase;

namespace Pipliz.APIProvider.Jobs
{
	public class CraftingJobSettings : IBlockJobSettings
	{
		public virtual ItemTypes.ItemType[] BlockTypes { get; set; }
		public virtual NPCType NPCType { get; set; }
		public virtual string NPCTypeKey { get; set; }
		public virtual InventoryItem RecruitmentItem { get; set; }
		public virtual float CraftingCooldown { get; set; }
		public virtual int MaxCraftsPerHaul { get; set; }
		public virtual string OnCraftedAudio { get; set; }

		public virtual bool ToSleep { get { return TimeCycle.ShouldSleep; } }

		// buffer for crafting results - not threadsafe but hey npc's aren't threaded
		static protected List<InventoryItem> craftingResults = new List<InventoryItem>();

		public CraftingJobSettings () { }

		public CraftingJobSettings (string blockType, string npcTypeKey, float craftingCooldown = 5f, int maxCraftsPerHaul = 5, string onCraftedAudio = null)
		{
			if (blockType != null) {
				BlockTypes = new ItemTypes.ItemType[] { ItemTypes.GetType(blockType) };
			}
			NPCTypeKey = npcTypeKey;
			NPCType = NPCType.GetByKeyNameOrDefault(npcTypeKey);
			CraftingCooldown = craftingCooldown;
			MaxCraftsPerHaul = maxCraftsPerHaul;
			OnCraftedAudio = onCraftedAudio;
		}

		public virtual void OnGoalChanged (BlockJobInstance instance, NPCGoal oldGoal, NPCGoal newGoal)
		{
			if (oldGoal == NPCGoal.Job && instance.IsBusy) {
				instance.IsBusy = false;
				OnStopCrafting(instance);
			}
		}

		public virtual IList<Recipe> GetPossibleRecipes (IJob job)
		{
			return job.Owner.RecipeData.GetAvailableRecipes<Recipe>(NPCTypeKey);
		}

		public virtual Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			return instance.Position;
		}

		public virtual void OnStartCrafting (BlockJobInstance instance) { }
		public virtual void OnStopCrafting (BlockJobInstance instance) { }

		public virtual void OnNPCAtJob (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			CraftingJobInstance instance = (CraftingJobInstance)blockJobInstance;
			var NPC = instance.NPC;
			NPC.LookAt(instance.Position.Vector);
			state.JobIsDone = true;
			if (instance.SelectedRecipe != null) {
				if (instance.SelectedRecipeCount > 0 && instance.SelectedRecipe.IsPossible(instance.Owner, state.Inventory)) {
					// got a recipe, still making it, and it's still possible
					// so get on with it
					state.Inventory.Remove(instance.SelectedRecipe.Requirements);

					craftingResults.Clear();
					for (int i = 0; i < instance.SelectedRecipe.Results.Count; i++) {
						craftingResults.Add(instance.SelectedRecipe.Results[i]);
					}

					ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCCraftedRecipe, instance as IJob, instance.SelectedRecipe, craftingResults);

					if (craftingResults.Count > 0) {
						// add result to npc's inventory
						state.Inventory.Add(craftingResults);
						ushort typeToShow;
						typeToShow = craftingResults[0].Type;
						if (craftingResults.Count > 1) {
							// overly complicated code to get a weighted random item from the list to display
							int totalTypes = 0;
							for (int i = 0; i < craftingResults.Count; i++) {
								totalTypes += craftingResults[i].Amount;
							}
							totalTypes = Random.Next(0, totalTypes + 1);
							for (int i = 0; i < craftingResults.Count; i++) {
								totalTypes -= craftingResults[i].Amount;
								if (totalTypes <= 0) {
									typeToShow = craftingResults[i].Type;
									break;
								}
							}
						}

						state.SetIndicator(new Shared.IndicatorState(CraftingCooldown, typeToShow));
						if (OnCraftedAudio != null) {
							ServerManager.SendAudio(instance.Position.Vector, OnCraftedAudio);
						}
					} else {
						state.SetIndicator(new Shared.IndicatorState(CraftingCooldown, NPCIndicatorType.None));
					}

					if (!instance.IsBusy) {
						instance.IsBusy = true;
						OnStartCrafting(instance);
					}
					state.JobIsDone = false;
					instance.SelectedRecipeCount--;
				} else {
					// got a recipe, but made as many as desired or not possible anymore
					instance.SelectedRecipe = null;
					instance.SelectedRecipeCount = 0;
					if (!state.Inventory.IsEmpty) {
						instance.ShouldTakeItems = true;
					}
					state.SetCooldown(0.1);
					if (instance.IsBusy) {
						instance.IsBusy = false;
						OnStopCrafting(instance);
					}
				}
			} else {
				if (instance.IsBusy) {
					instance.IsBusy = false;
					OnStopCrafting(instance);
				}
				// no recipe set at the moment, so find one
				var recipeMatch = Recipe.MatchRecipe<Recipe, IList<Recipe>>(GetPossibleRecipes(instance), instance.Owner);
				switch (recipeMatch.MatchType) {
					case Recipe.RecipeMatchType.AllDone:
					case Recipe.RecipeMatchType.FoundMissingRequirements:
						if (!state.Inventory.IsEmpty) {
							// at job, got something in inventory, and no new things to make.
							// clear inventory before indicating something is missing
							instance.ShouldTakeItems = true;
							state.SetCooldown(0.3);
						} else {
							// indicate the missing item / being complete
							state.JobIsDone = false;
							float cooldown = Random.NextFloat(8f, 16f);
							if (recipeMatch.MatchType == Recipe.RecipeMatchType.AllDone) {
								state.SetIndicator(new Shared.IndicatorState(cooldown, BuiltinBlocks.ErrorIdle));
							} else {
								state.SetIndicator(new Shared.IndicatorState(cooldown, recipeMatch.FoundRecipe.FindMissingType(instance.Owner.Stockpile), true, false));
							}
						}
						break;
					case Recipe.RecipeMatchType.FoundCraftable:
						// at job, new recipe to make - go fetch required materials
						instance.SelectedRecipe = recipeMatch.FoundRecipe;
						instance.ShouldTakeItems = true;
						state.SetCooldown(0.3);
						break;
					default:
						Assert.IsTrue(false, "Unexpected RecipeMatchType: " + recipeMatch.MatchType);
						break;
				}
			}
		}

		public virtual void OnNPCAtStockpile (BlockJobInstance blockJobInstance, ref NPCState state)
		{
			CraftingJobInstance instance = (CraftingJobInstance)blockJobInstance;
			if (state.Inventory.IsEmpty) {
				Assert.IsTrue(instance.ShouldTakeItems);
			} else {
				state.Inventory.Dump(instance.Owner.Stockpile);
				state.SetCooldown(0.3);
			}
			state.JobIsDone = true;
			if (instance.ShouldTakeItems) {
				instance.ShouldTakeItems = false;
				var recipeMatch = Recipe.MatchRecipe<Recipe, IList<Recipe>>(GetPossibleRecipes(instance), instance.Owner);
				switch (recipeMatch.MatchType) {
					case Recipe.RecipeMatchType.FoundMissingRequirements:
					case Recipe.RecipeMatchType.AllDone:
						// at crate, dumped items, nothing new to make - will return to job to complain (ShouldTakeItems = false)
						state.SetCooldown(0.5);
						instance.SelectedRecipeCount = 0;
						break;
					case Recipe.RecipeMatchType.FoundCraftable:
						instance.SelectedRecipe = recipeMatch.FoundRecipe;
						instance.SelectedRecipeCount = Math.Min(recipeMatch.FoundRecipeCount, MaxCraftsPerHaul);
						var reqs = instance.SelectedRecipe.Requirements;
						for (int i = 0; i < reqs.Count; i++) {
							state.Inventory.Add(reqs[i] * instance.SelectedRecipeCount);
							instance.Owner.Stockpile.TryRemove(reqs[i] * instance.SelectedRecipeCount);
						}
						state.SetCooldown(0.5);
						break;
					default:
						Assert.IsTrue(false, "Unexpected RecipeMatchType: " + recipeMatch.MatchType);
						break;
				}
			}
		}
	}

}
