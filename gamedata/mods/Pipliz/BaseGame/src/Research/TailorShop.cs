using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class TailorShop : BaseResearchable
	{
		public TailorShop ()
		{
			key = "pipliz.baseresearch.tailorshop";
			icon = "gamedata/textures/icons/tailorshop.png";
			iterationCount = 3;
			AddIterationRequirement("coatedplanks");
			AddIterationRequirement("flax");
			AddIterationRequirement("coppertools");
			AddDependency("pipliz.baseresearch.flaxfarming");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.tailorshop"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.tailorshop"));
		}
	}
}
