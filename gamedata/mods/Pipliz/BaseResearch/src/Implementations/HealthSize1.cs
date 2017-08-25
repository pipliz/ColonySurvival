using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthSize1 : BaseResearchable
	{
		public HealthSize1 ()
		{
			key = "pipliz.baseresearch.healthsize1";
			icon = "baseresearch_healthsize1.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.SetTemporaryValue("pipliz.healthmax", 125f);
		}
	}
}
