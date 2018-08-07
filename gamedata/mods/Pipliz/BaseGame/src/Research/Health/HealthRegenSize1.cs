
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSize1 : BaseResearchable
	{
		public HealthRegenSize1 ()
		{
			key = "pipliz.baseresearch.healthregensize1";
			icon = "gamedata/textures/icons/baseresearch_healthregensize1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.sciencebaglife");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenmax", 50f);
		}
	}
}
