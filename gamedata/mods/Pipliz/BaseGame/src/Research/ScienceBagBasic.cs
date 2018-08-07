using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagBasic : BaseResearchable
	{
		public ScienceBagBasic ()
		{
			key = "pipliz.baseresearch.sciencebagbasic";
			icon = "gamedata/textures/icons/sciencebagbasic.png";
			iterationCount = 3;
			AddIterationRequirement("linenbag");
			AddIterationRequirement("coppertools");
			AddIterationRequirement("bronzeplate");
			AddIterationRequirement("bricks", 3);
			AddDependency("pipliz.baseresearch.technologisttable");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.technologist.sciencebagbasic"));
		}
	}
}
