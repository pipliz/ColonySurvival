using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class SplittingStump : BaseResearchable
	{
		public SplittingStump ()
		{
			key = "pipliz.baseresearch.splittingstump";
			icon = "gamedata/textures/icons/splittingstump.png";
			iterationCount = 3;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzeaxe");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.splittingstumptemperate"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.splittingstumptaiga"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.splittingstumptaiga"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.splittingstumptemperate"));
		}
	}
}
