using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagColony : BaseResearchable
	{
		public ScienceBagColony ()
		{
			key = "pipliz.baseresearch.sciencebagcolony";
			icon = "gamedata/textures/icons/sciencebagcolony.png";
			iterationCount = 5;
			AddIterationRequirement("bronzeanvil");
			AddIterationRequirement("fineryforge");
			AddIterationRequirement("matchlockgun");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.matchlockgun");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.technologist.sciencebagcolony"));
		}
	}
}
