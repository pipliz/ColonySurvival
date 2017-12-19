using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthSize4 : BaseResearchable
	{
		public HealthSize4 ()
		{
			key = "pipliz.baseresearch.healthsize4";
			icon = "gamedata/textures/icons/baseresearch_healthsize4.png";
			iterationCount = 100;
			AddIterationRequirement("sciencebagadvanced", 2);
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.healthsize3");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthmax", 200f);
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				manager.Player.SendHealthPacket();
			}
		}
	}
}
