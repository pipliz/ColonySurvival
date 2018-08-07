using Science;

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

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthmax", 200f);
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				for (int i = 0; i < manager.Colony.Owners.Length; i++) {
					if (manager.Colony.Owners[i].ShouldSendData) {
						manager.Colony.Owners[i].SendHealthPacket();
					}
				}
			}
		}
	}
}
