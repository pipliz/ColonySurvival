
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSize4 : BaseResearchable
	{
		public HealthRegenSize4 ()
		{
			key = "pipliz.baseresearch.healthregensize4";
			icon = "gamedata/textures/icons/baseresearch_healthregensize4.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced", 3);
			AddIterationRequirement("sciencebaglife", 5);
			AddDependency("pipliz.baseresearch.healthregensize3");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenmax", 100f);
		}
	}
}
