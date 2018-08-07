
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed3 : BaseResearchable
	{
		public HealthRegenSpeed3 ()
		{
			key = "pipliz.baseresearch.healthregenspeed3";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced", 3);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthregenspeed2");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenspeed", 6f);
		}
	}
}
