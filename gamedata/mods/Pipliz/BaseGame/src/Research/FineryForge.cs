using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class FineryForge : BaseResearchable
	{
		public FineryForge ()
		{
			key = "pipliz.baseresearch.fineryforge";
			icon = "gamedata/textures/icons/fineryforge.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("ironwrought");
			AddDependency("pipliz.baseresearch.bloomery");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.metalsmith.fineryforge"));
		}
	}
}
