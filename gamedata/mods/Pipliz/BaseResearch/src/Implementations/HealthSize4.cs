using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthSize4 : BaseResearchable
	{
		public HealthSize4 ()
		{
			key = "pipliz.baseresearch.healthsize4";
			icon = "baseresearch_healthsize4.png";
			iterationCount = 100;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.healthsize3");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.SetTemporaryValue("pipliz.healthmax", 200f);
		}
	}
}
