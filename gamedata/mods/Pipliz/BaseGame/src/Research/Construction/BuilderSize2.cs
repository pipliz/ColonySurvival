using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionBuilderSize2 : BaseResearchable
	{
		public ConstructionBuilderSize2 ()
		{
			key = "pipliz.baseresearch.constructionbuildersize2";
			icon = "gamedata/textures/icons/buildersize2.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagadvanced");
			AddDependency("pipliz.baseresearch.constructionbuildersize1");
			AddDependency("pipliz.baseresearch.sciencebagadvanced");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.builderlimit", 100000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
