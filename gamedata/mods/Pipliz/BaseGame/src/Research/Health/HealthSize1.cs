
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthSize1 : BaseResearchable
	{
		public HealthSize1 ()
		{
			key = "pipliz.baseresearch.healthsize1";
			icon = "gamedata/textures/icons/baseresearch_healthsize1.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.sciencebaglife");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthmax", 125f);
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
