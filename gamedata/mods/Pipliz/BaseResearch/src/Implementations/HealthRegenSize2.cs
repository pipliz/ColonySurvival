using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSize2 : BaseResearchable
	{
		public HealthRegenSize2 ()
		{
			key = "pipliz.baseresearch.healthregensize2";
			icon = "baseresearch_healthregensize2.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregensize1");
		}
	}
}
