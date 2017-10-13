using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class TechnologistTable : BaseResearchable
	{
		public TechnologistTable ()
		{
			key = "pipliz.baseresearch.technologisttable";
			icon = "gamedata/textures/icons/technologisttable.png";
			iterationCount = 3;
			AddIterationRequirement("coatedplanks");
			AddIterationRequirement("bronzeplate", 2);
			AddIterationRequirement("coppernails");
			AddDependency("pipliz.baseresearch.tailorshop");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.technologisttable", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.technologisttable");
		}
	}
}
