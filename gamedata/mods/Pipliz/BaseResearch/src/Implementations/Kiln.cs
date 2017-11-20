using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class Kiln : BaseResearchable
	{
		public Kiln ()
		{
			key = "pipliz.baseresearch.kiln";
			icon = "gamedata/textures/icons/kiln.png";
			iterationCount = 3;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzeplate");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.kiln", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.kiln");
		}
	}
}
