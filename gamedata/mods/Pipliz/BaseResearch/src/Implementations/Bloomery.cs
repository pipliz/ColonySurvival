using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class Bloomery : BaseResearchable
	{
		public Bloomery ()
		{
			key = "pipliz.baseresearch.bloomery";
			icon = "gamedata/textures/icons/bloomery.png";
			iterationCount = 20;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.bloomery", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.bloomery");
		}
	}
}
