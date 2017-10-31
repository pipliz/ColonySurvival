using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class BannerRadius3 : BaseResearchable
	{
		public BannerRadius3()
		{
			key = "pipliz.baseresearch.bannerradius3";
			icon = "gamedata/textures/icons/baseresearch_bannerrange3.png";
			iterationCount = 50;
			AddIterationRequirement("steelingot");
			AddIterationRequirement("silveringot");
			AddDependency("pipliz.baseresearch.bannerradius2");
			AddDependency("pipliz.baseresearch.fineryforge");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 70);
			BannerTracker.SendPacket(manager.Player);
		}
	}
}
