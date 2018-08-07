
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSize2 : BaseResearchable
	{
		public HealthRegenSize2 ()
		{
			key = "pipliz.baseresearch.healthregensize2";
			icon = "gamedata/textures/icons/baseresearch_healthregensize2.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregensize1");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenmax", 65f);
		}
	}
}
