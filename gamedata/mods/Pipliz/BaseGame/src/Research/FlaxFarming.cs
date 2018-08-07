using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class FlaxFarming : BaseResearchable
	{
		public FlaxFarming ()
		{
			key = "pipliz.baseresearch.flaxfarming";
			icon = "gamedata/textures/icons/flax.png";
			iterationCount = 3;
			AddIterationRequirement("berry", 3);
			AddIterationRequirement("coppernails");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.linseedoil"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.coatedplanks"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.adobe"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.bowstring"));

			if (reason == EResearchCompletionReason.ProgressCompleted) {
				manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.FlaxStage1, 100);
				for (int i = 0; i < manager.Colony.Owners.Length; i++) {
					if (manager.Colony.Owners[i].ShouldSendData) {
						Chatting.Chat.Send(manager.Colony.Owners[i], "You received 100 flax seeds!");
					}
				}
			}
		}
	}
}
