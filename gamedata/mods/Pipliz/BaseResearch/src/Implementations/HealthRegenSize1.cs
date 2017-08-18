using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSize1 : BaseResearchable
	{
		public HealthRegenSize1 ()
		{
			key = "pipliz.baseresearch.healthregensize1";
			icon = "baseresearch_healthregensize1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 2);
		}
	}
}
