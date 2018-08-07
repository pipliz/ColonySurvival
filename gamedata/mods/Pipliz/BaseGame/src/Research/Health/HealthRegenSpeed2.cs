
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed2 : BaseResearchable
	{
		public HealthRegenSpeed2 ()
		{
			key = "pipliz.baseresearch.healthregenspeed2";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed2.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic", 3);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthregenspeed1");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.healthregenspeed", 4f);
		}
	}
}
