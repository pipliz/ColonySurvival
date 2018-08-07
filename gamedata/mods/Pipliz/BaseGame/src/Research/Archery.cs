using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class Archery : BaseResearchable
	{
		public Archery ()
		{
			key = "pipliz.baseresearch.archery";
			icon = "gamedata/textures/icons/bow.png";
			iterationCount = 3;
			AddIterationRequirement("bronzearrow", 3);
			AddIterationRequirement("bowstring");
			AddDependency("pipliz.baseresearch.bronzeanvil");
			AddDependency("pipliz.baseresearch.flaxfarming");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.bow"));
		}
	}
}
