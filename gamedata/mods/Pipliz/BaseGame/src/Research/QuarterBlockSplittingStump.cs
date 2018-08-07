using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class QuarterBlockSplittingStump : BaseResearchable
	{
		public QuarterBlockSplittingStump ()
		{
			key = "pipliz.baseresearch.quarterblocksplittingstump";
			icon = "gamedata/textures/icons/quarterblockbrownlight.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.splittingstump");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.woodcutter.quarterblockbrowndark"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.woodcutter.quarterblockbrownlight"));
		}
	}
}
