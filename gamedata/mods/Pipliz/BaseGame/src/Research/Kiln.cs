using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class Kiln : BaseResearchable
	{
		public Kiln ()
		{
			key = "pipliz.baseresearch.kiln";
			icon = "gamedata/textures/icons/kiln.png";
			iterationCount = 3;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzeplate");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.kiln"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.kiln"));
		}
	}
}
