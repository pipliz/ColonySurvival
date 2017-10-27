using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class BannerRadius2 : BaseResearchable
	{
		public BannerRadius2()
		{
			key = "pipliz.baseresearch.bannerradius2";
			icon = "gamedata/textures/icons/banner.png";
			iterationCount = 25;
			AddIterationRequirement("crossbow");
			AddIterationRequirement("bronzeanvil");
			AddDependency("pipliz.baseresearch.bannerradius1");
			AddDependency("pipliz.baseresearch.crossbow");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 55);
			BannerTracker.SendPacket(manager.Player);
		}
	}
}
