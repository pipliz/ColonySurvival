using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class TailorBase : BaseResearchable
	{
		public TailorBase ()
		{
			key = "pipliz.baseresearch.tailorbase";
			icon = "gamedata/textures/icons/baseresearch_tailorbase.png";
			iterationCount = 15;
			AddIterationRequirement("flax");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.tailorshop", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.tailorshop");
		}
	}
}
