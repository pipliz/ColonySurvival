using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class BannerRadius4 : BaseResearchable
	{
		public BannerRadius4()
		{
			key = "pipliz.baseresearch.bannerradius4";
			icon = "gamedata/textures/icons/baseresearch_bannerrange4.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.bannerradius3");
			AddDependency("pipliz.baseresearch.sciencebagadvanced");
			AddDependency("pipliz.baseresearch.sciencebagcolony");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 85);
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				BannerTracker.SendPacket(manager.Player);
			}
		}
	}
}
