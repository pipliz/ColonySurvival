using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	public class TestResearchable2 : BaseResearchable
	{
		public TestResearchable2 ()
		{
			key = "pipliz.baseresearch.testresearchable2";
			iterationCount = 3;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.testreachable");
		}

		public override void OnResearchComplete ()
		{
			Log.Write("{0} completed", key);
		}
	}
}
