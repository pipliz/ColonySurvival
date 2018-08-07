using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagLife : BaseResearchable
	{
		public ScienceBagLife ()
		{
			key = "pipliz.baseresearch.sciencebaglife";
			icon = "gamedata/textures/icons/sciencebaglife.png";
			iterationCount = 3;
			AddIterationRequirement("flour");
			AddIterationRequirement("berry", 5);
			AddIterationRequirement("clothing");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.technologisttable");
			AddDependency("pipliz.baseresearch.wheatfarming");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.technologist.sciencebaglife"));
		}
	}
}
