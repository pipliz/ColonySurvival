using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSize3 : BaseResearchable
	{
		public HealthRegenSize3 ()
		{
			key = "pipliz.baseresearch.healthregensize3";
			icon = "gamedata/textures/icons/baseresearch_healthregensize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregensize2");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthregenmax", 85f);
		}
	}
}
