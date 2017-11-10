using NPC;
using Pipliz.JSON;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Pipliz.APIProvider.Jobs
{
	public class CraftingJobBase : BlockJobBase, IRecipeLimitsProvider
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
			blockInventory = new NPCInventory(node["inventory"]);
			InitializeJob(player, (Vector3Int)node["position"], node.GetAs<int>("npcID"));
			return this;
		}

		public virtual void OnStartCrafting () { wasCrafting = true; }

		public virtual void OnStopCrafting () { wasCrafting = false; }

		public override bool NeedsItems { get { return shouldTakeItems; } }

		public virtual IList<Recipe> GetPossibleRecipes { get { return RecipeStorage.GetPlayerStorage(owner).GetAvailableRecipes<Recipe>(NPCTypeKey); } }

		public virtual int MaxRecipeCraftsPerHaul { get { throw new System.NotImplementedException(); } }

		public virtual float CraftingCooldown
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}

		public override void OnNPCAtJob (ref NPCBase.NPCState state)
		{
			state.JobIsDone = true;
			usedNPC.LookAt(position.Vector);
			if (!state.Inventory.IsEmpty) {
				usedNPC.Inventory.Dump(blockInventory);
			}
			if (selectedRecipe != null) {
				if (recipesToCraft > 0 && selectedRecipe.IsPossible(usedNPC.Colony.UsedStockpile, blockInventory)) {
					blockInventory.Remove(selectedRecipe.Requirements);
					blockInventory.Add(selectedRecipe.Results);
					state.SetIndicator(NPCIndicatorType.Crafted, CraftingCooldown, selectedRecipe.Results[0].Type);
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
				var recipeMatch = Recipe.MatchRecipe<Recipe, IList<Recipe>>(GetPossibleRecipes, usedNPC.Colony.UsedStockpile);
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
								state.SetIndicator(NPCIndicatorType.SuccessIdle, cooldown);
							} else {
								state.SetIndicator(NPCIndicatorType.MissingItem, cooldown, recipeMatch.FoundRecipe.FindMissingType(owner));
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
				state.Inventory.TryDump(usedNPC.Colony.UsedStockpile);
				state.SetCooldown(0.3);
			}
			state.JobIsDone = true;
			if (shouldTakeItems) {
				shouldTakeItems = false;
				var recipeMatch = Recipe.MatchRecipe<Recipe, IList<Recipe>>(GetPossibleRecipes, usedNPC.Colony.UsedStockpile);
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
							usedNPC.Colony.UsedStockpile.TryRemove(selectedRecipe.Requirements[i] * recipesToCraft);
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

		protected virtual string GetRecipeLocation ()
		{
			throw new System.NotImplementedException();
		}

		// IRecipeLimitsProvider
		public virtual IList<Recipe> GetCraftingLimitsRecipes ()
		{
			return Recipe.LoadRecipes(GetRecipeLocation());
		}

		// IRecipeLimitsProvider
		public virtual List<string> GetCraftingLimitsTriggers ()
		{
			return null;
		}

		// IRecipeLimitsProvider
		public virtual string GetCraftingLimitsType ()
		{
			return NPCTypeKey;
		}
	}
}
