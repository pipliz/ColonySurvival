using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class BannerRadius1 : BaseResearchable
	{
		public BannerRadius1()
		{
			key = "pipliz.baseresearch.bannerradius1";
			icon = "gamedata/textures/icons/baseresearch_bannerrange1.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
			AddDependency("pipliz.baseresearch.sciencebaglife");
			AddDependency("pipliz.baseresearch.sciencebagmilitary");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.bannersaferadius", 40);
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				BannerTracker.SendPacket(manager.Player);
			}
		}
	}
}
