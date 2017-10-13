using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthSize3 : BaseResearchable
	{
		public HealthSize3 ()
		{
			key = "pipliz.baseresearch.healthsize3";
			icon = "gamedata/textures/icons/baseresearch_healthsize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.healthsize2");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.SetTemporaryValue("pipliz.healthmax", 175f);
			manager.Player.SendHealthPacket();
		}
	}
}
