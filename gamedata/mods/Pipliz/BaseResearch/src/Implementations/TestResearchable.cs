using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	public class TestResearchable : BaseResearchable
	{
		public TestResearchable ()
		{
			key = "pipliz.baseresearch.testresearchable";
			iterationCount = 5;
			AddIterationRequirement("sciencebagbasic");
		}

		public override void OnResearchComplete ()
		{
			Log.Write("{0} completed", key);
		}
	}
}
