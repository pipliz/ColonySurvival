using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSize4 : BaseResearchable
	{
		public HealthRegenSize4 ()
		{
			key = "pipliz.baseresearch.healthregensize4";
			icon = "baseresearch_healthregensize4.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic", 3);
			AddIterationRequirement("sciencebaglife", 5);
			AddDependency("pipliz.baseresearch.healthregensize3");
		}
	}
}
