using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed1 : BaseResearchable
	{
		public HealthRegenSpeed1 ()
		{
			key = "pipliz.baseresearch.healthregenspeed1";
			icon = "baseresearch_healthregenspeed1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.SetTemporaryValue("pipliz.healthregenspeed", 2f);
		}
	}
}
