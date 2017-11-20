using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSize1 : BaseResearchable
	{
		public HealthRegenSize1 ()
		{
			key = "pipliz.baseresearch.healthregensize1";
			icon = "gamedata/textures/icons/baseresearch_healthregensize1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.sciencebaglife");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthregenmax", 50f);
		}
	}
}
