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
			icon = "baseresearch_healthregensize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregensize2");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.SetTemporaryValue("pipliz.healthregenmax", 85f);
		}
	}
}
