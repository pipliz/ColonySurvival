using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class BronzeMinting : BaseResearchable
	{
		public BronzeMinting ()
		{
			key = "pipliz.baseresearch.bronzeminting";
			icon = "gamedata/textures/icons/bronzecoin.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzeingot");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.mint"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.shop"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.mint"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.shop"));
		}
	}
}
