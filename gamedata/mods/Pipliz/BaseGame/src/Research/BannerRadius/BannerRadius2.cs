using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class BannerRadius2 : BaseResearchable
	{
		public BannerRadius2()
		{
			key = "pipliz.baseresearch.bannerradius2";
			icon = "gamedata/textures/icons/baseresearch_bannerrange2.png";
			iterationCount = 25;
			AddIterationRequirement("crossbow");
			AddIterationRequirement("bronzeanvil");
			AddDependency("pipliz.baseresearch.bannerradius1");
			AddDependency("pipliz.baseresearch.crossbow");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 55);
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				BannerTracker.SendPacket(manager.Player);
			}
		}
	}
}
