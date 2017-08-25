using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed4 : BaseResearchable
	{
		public HealthRegenSpeed4 ()
		{
			key = "pipliz.baseresearch.healthregenspeed4";
			icon = "baseresearch_healthregenspeed4.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic", 5);
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregenspeed3");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.SetTemporaryValue("pipliz.healthregenspeed", 8f);
		}
	}
}
