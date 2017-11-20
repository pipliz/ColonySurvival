using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class ScienceBagMilitary : BaseResearchable
	{
		public ScienceBagMilitary ()
		{
			key = "pipliz.baseresearch.sciencebagmilitary";
			icon = "gamedata/textures/icons/sciencebagmilitary.png";
			iterationCount = 3;
			AddIterationRequirement("ironsword");
			AddIterationRequirement("bronzearrow", 3);
			AddIterationRequirement("bow");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.technologisttable");
			AddDependency("pipliz.baseresearch.bloomery");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagmilitary", true, "pipliz.technologist");
		}
	}
}
