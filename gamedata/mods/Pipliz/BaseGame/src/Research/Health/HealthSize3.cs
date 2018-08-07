
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthSize3 : BaseResearchable
	{
		public HealthSize3 ()
		{
			key = "pipliz.baseresearch.healthsize3";
			icon = "gamedata/textures/icons/baseresearch_healthsize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced", 2);
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.healthsize2");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthmax", 175f);
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
