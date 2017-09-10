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
			icon = "baseresearch_sciencebaglife.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bread");
			AddIterationRequirement("berry");
			AddDependency("pipliz.baseresearch.technologistbase");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebaglife", true, "pipliz.technologist");
		}
	}
}
