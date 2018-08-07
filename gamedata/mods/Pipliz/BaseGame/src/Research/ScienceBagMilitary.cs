using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagMilitary : BaseResearchable
	{
		public ScienceBagMilitary ()
		{
			key = "pipliz.baseresearch.sciencebagmilitary";
			icon = "gamedata/textures/icons/sciencebagmilitary.png";
			iterationCount = 3;
			AddIterationRequirement("ironsword");
			AddIterationRequirement("bronzearrow", 3);
			AddIterationRequirement("bow");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.technologisttable");
			AddDependency("pipliz.baseresearch.bloomery");
			AddDependency("pipliz.baseresearch.archery");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.technologist.sciencebagmilitary"));
		}
	}
}
