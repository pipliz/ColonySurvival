using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class CrossBow : BaseResearchable
	{
		public CrossBow ()
		{
			key = "pipliz.baseresearch.crossbow";
			icon = "gamedata/textures/icons/crossbow.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.crossbowbolt");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.metalsmith.crossbow"));
		}
	}
}
