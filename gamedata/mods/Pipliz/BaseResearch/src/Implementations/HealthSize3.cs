using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthSize3 : BaseResearchable
	{
		public HealthSize3 ()
		{
			key = "pipliz.baseresearch.healthsize3";
			icon = "baseresearch_healthsize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife", 2);
			AddDependency("pipliz.baseresearch.healthsize2");
		}
	}
}
