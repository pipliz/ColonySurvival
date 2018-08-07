using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class DyerTable : BaseResearchable
	{
		public DyerTable ()
		{
			key = "pipliz.baseresearch.dyertable";
			icon = "gamedata/textures/icons/dyertable.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("hollyhock");
			AddDependency("pipliz.baseresearch.herbfarming");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.crafter.dyertable"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.player.dyertable"));
		}
	}
}
