
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed1 : BaseResearchable
	{
		public HealthRegenSpeed1 ()
		{
			key = "pipliz.baseresearch.healthregenspeed1";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.sciencebaglife");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenspeed", 2f);
		}
	}
}
