using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
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

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 100);
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				BannerTracker.SendPacket(manager.Player);
			}
		}
	}
}
