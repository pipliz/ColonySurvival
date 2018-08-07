using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagAdvanced : BaseResearchable
	{
		public ScienceBagAdvanced ()
		{
			key = "pipliz.baseresearch.sciencebagadvanced";
			icon = "gamedata/textures/icons/sciencebagadvanced.png";
			iterationCount = 5;
			AddIterationRequirement("steelparts");
			AddIterationRequirement("gunpowderpouch");
			AddIterationRequirement("silveringot");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.gunpowder");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.technologist.sciencebagadvanced"));
		}
	}
}
