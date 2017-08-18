using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed3 : BaseResearchable
	{
		public HealthRegenSpeed3 ()
		{
			key = "pipliz.baseresearch.healthregenspeed3";
			icon = "baseresearch_healthregenspeed3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic", 3);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.healthregenspeed2");
		}
	}
}
