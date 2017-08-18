using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed1 : BaseResearchable
	{
		public HealthRegenSpeed1 ()
		{
			key = "pipliz.baseresearch.healthregenspeed1";
			icon = "baseresearch_healthregenspeed1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife");
		}
	}
}
