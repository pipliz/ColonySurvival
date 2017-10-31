using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class BannerRadius5 : BaseResearchable
	{
		public BannerRadius5()
		{
			key = "pipliz.baseresearch.bannerradius5";
			icon = "gamedata/textures/icons/baseresearch_bannerrange5.png";
			iterationCount = 100;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.bannerradius4");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 100);
			BannerTracker.SendPacket(manager.Player);
		}
	}
}
