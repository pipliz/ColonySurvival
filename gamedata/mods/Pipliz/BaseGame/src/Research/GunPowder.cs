using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class GunPowder : BaseResearchable
	{
		public GunPowder ()
		{
			key = "pipliz.baseresearch.gunpowder";
			icon = "gamedata/textures/icons/gunpowder.png";
			iterationCount = 25;
			AddIterationRequirement("salpeter");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.gunsmithshop");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.gunsmith.gunpowder"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.gunsmith.gunpowderpouch"));
		}
	}
}
