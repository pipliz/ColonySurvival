using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class Lantern : BaseResearchable
	{
		public Lantern ()
		{
			key = "pipliz.baseresearch.lantern";
			icon = "gamedata/textures/icons/lanternyellow.png";
			iterationCount = 5;
			AddIterationRequirement("sciencebagbasic", 5);
			AddIterationRequirement("ironrivet");
			AddIterationRequirement("crystal");
			AddDependency("pipliz.baseresearch.bloomery");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.metalsmith.lanternyellow"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanternwhite"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanterngreen"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanternblue"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanternred"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanternorange"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanterncyan"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.dyer.lanternpink"));

		}
	}
}
