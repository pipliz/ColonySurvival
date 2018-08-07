using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class BronzeAnvil : BaseResearchable
	{
		public BronzeAnvil ()
		{
			key = "pipliz.baseresearch.bronzeanvil";
			icon = "gamedata/textures/icons/bronzeanvil.png";
			iterationCount = 3;
			AddIterationRequirement("bronzeingot");
			AddIterationRequirement("coppertools");
			AddIterationRequirement("coppernails");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.bronzeanvil"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.bronzeanvil"));
		}
	}
}
