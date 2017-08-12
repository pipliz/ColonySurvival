using Server.Science;

namespace Pipliz.BaseResearch
{
	[ModLoader.ModManager]
	public class ModEntries
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAddResearchables, "pipliz.baseresearch.add")]
		public static void OnAddResearchables ()
		{
			ScienceManager.RegisterResearchable(new Implementations.TestResearchable());
			ScienceManager.RegisterResearchable(new Implementations.TestResearchable2());
		}
	}
}