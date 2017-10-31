using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class ScienceBagColony : BaseResearchable
	{
		public ScienceBagColony ()
		{
			key = "pipliz.baseresearch.sciencebagcolony";
			icon = "gamedata/textures/icons/sciencebagcolony.png";
			iterationCount = 5;
			AddIterationRequirement("bronzeanvil");
			AddIterationRequirement("fineryforge");
			AddIterationRequirement("matchlockgun");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.matchlockgun");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagcolony", true, "pipliz.technologist");
		}
	}
}
