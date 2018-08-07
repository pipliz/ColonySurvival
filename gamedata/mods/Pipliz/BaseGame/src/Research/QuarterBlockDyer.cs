using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class QuarterBlockDyer : BaseResearchable
	{
		public QuarterBlockDyer ()
		{
			key = "pipliz.baseresearch.quarterblockdyer";
			icon = "gamedata/textures/icons/quarterblockwhite.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("gypsum");
			AddIterationRequirement("charcoal");
			AddDependency("pipliz.baseresearch.dyertable");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.quarterblockblack"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.quarterblockwhite"));
		}
	}
}
