using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class ScienceBagLife : BaseResearchable
	{
		public ScienceBagLife ()
		{
			key = "pipliz.baseresearch.sciencebaglife";
			icon = "gamedata/textures/icons/sciencebaglife.png";
			iterationCount = 3;
			AddIterationRequirement("flour");
			AddIterationRequirement("berry", 5);
			AddIterationRequirement("clothing");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.technologisttable");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebaglife", true, "pipliz.technologist");
		}
	}
}
