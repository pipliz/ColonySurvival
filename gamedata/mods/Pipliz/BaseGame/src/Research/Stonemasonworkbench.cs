using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class StonemasonWorkbench : BaseResearchable
	{
		public StonemasonWorkbench ()
		{
			key = "pipliz.baseresearch.stonemasonworkbench";
			icon = "gamedata/textures/icons/stonemasonworkbench.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.stonemasonworkbench"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.stonemasonworkbench"));
		}
	}
}
