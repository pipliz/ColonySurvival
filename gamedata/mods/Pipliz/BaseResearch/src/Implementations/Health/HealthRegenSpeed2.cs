using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed2 : BaseResearchable
	{
		public HealthRegenSpeed2 ()
		{
			key = "pipliz.baseresearch.healthregenspeed2";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed2.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic", 3);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthregenspeed1");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthregenspeed", 4f);
		}
	}
}
