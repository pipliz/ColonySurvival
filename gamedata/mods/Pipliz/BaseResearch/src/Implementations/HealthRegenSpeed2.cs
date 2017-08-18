using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed2 : BaseResearchable
	{
		public HealthRegenSpeed2 ()
		{
			key = "pipliz.baseresearch.healthregenspeed2";
			icon = "baseresearch_healthregenspeed2.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic", 3);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthregenspeed1");
		}
	}
}
