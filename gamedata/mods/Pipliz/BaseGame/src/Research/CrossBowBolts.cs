using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class CrossBowBolt : BaseResearchable
	{
		public CrossBowBolt ()
		{
			key = "pipliz.baseresearch.crossbowbolt";
			icon = "gamedata/textures/icons/crossbowbolt.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.sciencebagmilitary");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.metalsmith.crossbowbolt"));
		}
	}
}
