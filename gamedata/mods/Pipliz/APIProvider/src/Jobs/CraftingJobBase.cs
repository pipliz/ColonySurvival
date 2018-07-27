using NPC;
using Pipliz.JSON;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Pipliz.Mods.APIProvider.Jobs
{
	public class CraftingJobBase : BlockJobBase
	{
		protected NPCInventory blockInventory;
		protected bool shouldTakeItems;
		protected Recipe selectedRecipe;
		protected int recipesToCraft;
		protected bool wasCrafting;

		public override JSONNode GetJSON ()
		{
			return base.GetJSON()
				.SetAs("inventory", blockInventory.GetJSON());
		}

		public virtual ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			blockInventory = new NPCInventory(10000000f);
			InitializeJob(player, position, 0);
			return this;
		}

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			blockInventory = new NPCInventory(10000000f, node["inventory"]);
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			return this;
		}

		public virtual NPCInventory BlockInventory { get { return blockInventory; } }

		public virtual void OnStartCrafting () { wasCrafting = true; }

		public virtual void OnStopCrafting () { wasCrafting = false; }

		public override bool NeedsItems { get { return shouldTakeItems; } }

		public virtual IList<Recipe> GetPossibleRecipes { get { return RecipeStorage.GetColonyStorage(owner).GetAvailableRecipes<Recipe>(NPCTypeKey); } }

		public virtual int MaxRecipeCraftsPerHaul { get { throw new System.NotImplementedException(); } }

		public virtual float CraftingCooldown
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}

		static List<InventoryItem> craftingResults = new List<InventoryItem>();

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			state.JobIsDone = true;
			usedNPC.LookAt(position.Vector);
			if (!state.Inventory.IsEmpty) {
				usedNPC.Inventory.Dump(blockInventory);
			}
			if (selectedRecipe != null) {
				if (recipesToCraft > 0 && selectedRecipe.IsPossible(usedNPC.Colony.Stockpile, blockInventory)) {
					blockInventory.Remove(selectedRecipe.Requirements);

					craftingResults.Clear();
					for (int i = 0; i < selectedRecipe.Results.Count; i++) {
						craftingResults.Add(selectedRecipe.Results[i]);
					}
					ModLoader.TriggerCallbacks(ModLoader.EModCallbackType.OnNPCCraftedRecipe, (IJob)this, selectedRecipe, craftingResults);

					if (craftingResults.Count > 0) {
						blockInventory.Add(craftingResults);
						ushort typeToShow;
						typeToShow = craftingResults[0].Type;
						if (craftingResults.Count > 1) {
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
					} else {
						state.SetIndicator(new Shared.IndicatorState(CraftingCooldown, NPCIndicatorType.None));
					}

					state.JobIsDone = false;
					recipesToCraft--;
					OnRecipeCrafted();
				} else {
					selectedRecipe = null;
					recipesToCraft = 0;
					blockInventory.Dump(usedNPC.Inventory);
					if (!state.Inventory.IsEmpty) {
						shouldTakeItems = true;
					}
					state.SetCooldown(0.1);
					if (wasCrafting) {
						OnStopCrafting();
					}
				}
			} else {
				var recipeMatch = Recipe.MatchRecipe<Recipe, IList<Recipe>>(GetPossibleRecipes, usedNPC.Colony);
				switch (recipeMatch.MatchType) {
					case Recipe.RecipeMatchType.AllDone:
					case Recipe.RecipeMatchType.FoundMissingRequirements:
						if (!state.Inventory.IsEmpty || !blockInventory.IsEmpty) {
							blockInventory.Dump(usedNPC.Inventory);
							shouldTakeItems = true;
							state.SetCooldown(0.3);
						} else {
							state.JobIsDone = false;
							float cooldown = Random.NextFloat(8f, 16f);
							if (recipeMatch.MatchType == Recipe.RecipeMatchType.AllDone) {
								state.SetIndicator(new Shared.IndicatorState(cooldown, BlockTypes.Builtin.BuiltinBlocks.ErrorIdle));
							} else {
								state.SetIndicator(new Shared.IndicatorState(cooldown, recipeMatch.FoundRecipe.FindMissingType(owner), true, false));
							}
						}
						break;
					case Recipe.RecipeMatchType.FoundCraftable:
						blockInventory.Dump(usedNPC.Inventory);
						selectedRecipe = recipeMatch.FoundRecipe;
						shouldTakeItems = true;
						state.SetCooldown(0.3);
						break;
					default:
						Assert.IsTrue(false, "Unexpected RecipeMatchType: " + recipeMatch.MatchType);
						break;
				}
				if (wasCrafting) {
					OnStopCrafting();
				}
			}
		}

		public override void OnNPCAtStockpile (ref NPCBase.NPCState state)
		{
			if (state.Inventory.IsEmpty) {
				Assert.IsTrue(shouldTakeItems);
			} else {
				state.Inventory.Dump(usedNPC.Colony.Stockpile);
				state.SetCooldown(0.3);
			}
			state.JobIsDone = true;
			if (shouldTakeItems) {
				shouldTakeItems = false;
				var recipeMatch = Recipe.MatchRecipe<Recipe, IList<Recipe>>(GetPossibleRecipes, usedNPC.Colony);
				switch (recipeMatch.MatchType) {
					case Recipe.RecipeMatchType.FoundMissingRequirements:
					case Recipe.RecipeMatchType.AllDone:
						state.SetCooldown(0.5);
						recipesToCraft = 0;
						break;
					case Recipe.RecipeMatchType.FoundCraftable:
						selectedRecipe = recipeMatch.FoundRecipe;
						recipesToCraft = Math.Min(recipeMatch.FoundRecipeCount, MaxRecipeCraftsPerHaul);
						for (int i = 0; i < selectedRecipe.Requirements.Count; i++) {
							state.Inventory.Add(selectedRecipe.Requirements[i] * recipesToCraft);
							usedNPC.Colony.Stockpile.TryRemove(selectedRecipe.Requirements[i] * recipesToCraft);
						}
						state.SetCooldown(0.5);
						break;
					default:
						Assert.IsTrue(false, "Unexpected RecipeMatchType: " + recipeMatch.MatchType);
						break;
				}
			}
		}

		protected virtual void OnRecipeCrafted ()
		{
			if (!wasCrafting) {
				OnStartCrafting();
			}
		}

		protected override void OnChangedGoal (NPCBase.NPCGoal oldGoal, NPCBase.NPCGoal newGoal)
		{
			if (oldGoal == NPCBase.NPCGoal.Job && wasCrafting) {
				OnStopCrafting();
			}
		}
	}
}
