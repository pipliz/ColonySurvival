using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionBuilderSize1 : BaseResearchable
	{
		public ConstructionBuilderSize1 ()
		{
			key = "pipliz.baseresearch.constructionbuildersize1";
			icon = "gamedata/textures/icons/buildersize1.png";
			iterationCount = 30;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("stoneblock", 5);
			AddDependency("pipliz.baseresearch.constructionbuilder");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.builderlimit", 25000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
