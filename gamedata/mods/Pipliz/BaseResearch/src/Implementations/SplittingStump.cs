using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class SplittingStump : BaseResearchable
	{
		public SplittingStump ()
		{
			key = "pipliz.baseresearch.splittingstump";
			icon = "gamedata/textures/icons/splittingstump.png";
			iterationCount = 3;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzeaxe");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.splittingstumptemperate", true, "pipliz.crafter");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.splittingstumptaiga", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.splittingstumptaiga");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.splittingstumptemperate");
		}
	}
}
