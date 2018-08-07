using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class Bloomery : BaseResearchable
	{
		public Bloomery ()
		{
			key = "pipliz.baseresearch.bloomery";
			icon = "gamedata/textures/icons/bloomery.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.kiln");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.bloomery"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.bloomery"));
		}
	}
}
