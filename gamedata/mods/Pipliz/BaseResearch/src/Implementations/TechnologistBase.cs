using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class TechnologistBase : BaseResearchable
	{
		public TechnologistBase ()
		{
			key = "pipliz.baseresearch.technologistbase";
			icon = "gamedata/textures/icons/baseresearch_technologistbase.png";
			iterationCount = 15;
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.tailorbase");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.technologisttable", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.technologisttable");
		}
	}
}
