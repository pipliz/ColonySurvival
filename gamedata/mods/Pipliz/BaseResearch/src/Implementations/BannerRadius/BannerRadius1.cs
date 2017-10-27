using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class BannerRadius1 : BaseResearchable
	{
		public BannerRadius1()
		{
			key = "pipliz.baseresearch.bannerradius1";
			icon = "gamedata/textures/icons/banner.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.sciencebagmilitary");
			AddDependency("pipliz.baseresearch.sciencebaglife");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 40);
			BannerTracker.SendPacket(manager.Player);
		}
	}
}
