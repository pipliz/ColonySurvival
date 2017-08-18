using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSize3 : BaseResearchable
	{
		public HealthRegenSize3 ()
		{
			key = "pipliz.baseresearch.healthregensize3";
			icon = "baseresearch_healthregensize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife", 3);
			AddDependency("pipliz.baseresearch.healthregensize2");
		}
	}
}
