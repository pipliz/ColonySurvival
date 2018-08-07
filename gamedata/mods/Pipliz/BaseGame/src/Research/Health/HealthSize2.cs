
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthSize2 : BaseResearchable
	{
		public HealthSize2 ()
		{
			key = "pipliz.baseresearch.healthsize2";
			icon = "gamedata/textures/icons/baseresearch_healthsize2.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthsize1");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthmax", 150f);
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
