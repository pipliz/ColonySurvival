using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthSize1 : BaseResearchable
	{
		public HealthSize1 ()
		{
			key = "pipliz.baseresearch.healthsize1";
			icon = "baseresearch_healthsize1.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
		}
	}
}
