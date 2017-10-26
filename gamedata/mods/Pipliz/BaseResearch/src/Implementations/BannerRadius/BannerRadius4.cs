using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class BannerRadius4 : BaseResearchable
	{
		public BannerRadius4()
		{
			key = "pipliz.baseresearch.bannerradius4";
			icon = "gamedata/textures/icons/banner.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.bannerradius3");
			AddDependency("pipliz.baseresearch.sciencebagadvanced");
			AddDependency("pipliz.baseresearch.sciencebagcolony");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 85);
			BannerTracker.SendPacket(manager.Player);
		}
	}
}
