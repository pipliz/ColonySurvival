using Pipliz.APIProvider.Science;

namespace Pipliz.BaseResearch.Implementations
{
	public class TestResearchable2 : BaseResearchable
	{
		public TestResearchable2 ()
		{
			AddDependency("pipliz.baseresearch.testresearchable");
		}

		public override string GetKey ()
		{
			return "pipliz.baseresearch.testresearchable2";
		}
	}
}
