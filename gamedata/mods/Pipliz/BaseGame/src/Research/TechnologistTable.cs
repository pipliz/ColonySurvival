using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class TechnologistTable : BaseResearchable
	{
		public TechnologistTable ()
		{
			key = "pipliz.baseresearch.technologisttable";
			icon = "gamedata/textures/icons/technologisttable.png";
			iterationCount = 3;
			AddIterationRequirement("coatedplanks");
			AddIterationRequirement("bronzeplate", 2);
			AddIterationRequirement("coppernails");
			AddDependency("pipliz.baseresearch.tailorshop");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.technologisttable"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.technologisttable"));
		}
	}
}
