using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class QuarterBlockStonemason : BaseResearchable
	{
		public QuarterBlockStonemason ()
		{
			key = "pipliz.baseresearch.quarterblockstonemason";
			icon = "gamedata/textures/icons/quarterblockgrey.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.stonemasonworkbench");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.stonemason.quarterblockgrey"));
		}
	}
}
