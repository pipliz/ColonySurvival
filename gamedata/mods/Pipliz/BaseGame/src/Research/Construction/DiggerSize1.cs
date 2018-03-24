using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDiggerSize1 : BaseResearchable
	{
		public ConstructionDiggerSize1 ()
		{
			key = "pipliz.baseresearch.constructiondiggersize1";
			icon = "gamedata/textures/icons/diggersize1.png";
			iterationCount = 30;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("stoneblock", 5);
			AddDependency("pipliz.baseresearch.constructiondigger");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.diggerlimit", 25000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
