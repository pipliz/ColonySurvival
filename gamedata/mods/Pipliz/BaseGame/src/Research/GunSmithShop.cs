using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class GunSmithShop : BaseResearchable
	{
		public GunSmithShop ()
		{
			key = "pipliz.baseresearch.gunsmithshop";
			icon = "gamedata/textures/icons/gunsmithshop.png";
			iterationCount = 25;
			AddIterationRequirement("lead");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.fineryforge");
			AddDependency("pipliz.baseresearch.sciencebagmilitary");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.gunsmithshop"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.gunsmithshop"));
		}
	}
}
