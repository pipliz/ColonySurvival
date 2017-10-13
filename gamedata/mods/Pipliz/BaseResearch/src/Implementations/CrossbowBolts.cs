using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class CrossBowBolt : BaseResearchable
	{
		public CrossBowBolt ()
		{
			key = "pipliz.baseresearch.crossbowbolt";
			icon = "gamedata/textures/icons/crossbowbolt.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.bloomery");
			AddDependency("pipliz.baseresearch.sciencebagmilitary");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.metalsmith.crossbowbolt", true, "pipliz.metalsmith");
		}
	}
}
