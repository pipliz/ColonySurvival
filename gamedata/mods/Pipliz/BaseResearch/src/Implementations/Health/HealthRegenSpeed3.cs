using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed3 : BaseResearchable
	{
		public HealthRegenSpeed3 ()
		{
			key = "pipliz.baseresearch.healthregenspeed3";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced", 3);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthregenspeed2");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthregenspeed", 6f);
		}
	}
}
