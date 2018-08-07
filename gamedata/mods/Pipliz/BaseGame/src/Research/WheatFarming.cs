using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class WheatFarming : BaseResearchable
	{
		public WheatFarming ()
		{
			key = "pipliz.baseresearch.wheatfarming";
			icon = "gamedata/textures/icons/bread.png";
			iterationCount = 5;
			AddIterationRequirement("berry", 10);
			AddIterationRequirement("copperparts", 2);
			AddIterationRequirement("coppertools");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.oven"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.grindstone"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.oven"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.grindstone"));

			if (reason == EResearchCompletionReason.ProgressCompleted) {
				manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.WheatStage1, 400);
				for (int i = 0; i < manager.Colony.Owners.Length; i++) {
					if (manager.Colony.Owners[i].ShouldSendData) {
						Chatting.Chat.Send(manager.Colony.Owners[i], "You received 400 wheat seeds!");
					}
				}
			}
		}
	}
}
