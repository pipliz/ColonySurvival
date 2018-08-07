
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed4 : BaseResearchable
	{
		public HealthRegenSpeed4 ()
		{
			key = "pipliz.baseresearch.healthregenspeed4";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed4.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced", 5);
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregenspeed3");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenspeed", 8f);
		}
	}
}
