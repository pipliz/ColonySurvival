using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionBuilderSize4 : BaseResearchable
	{
		public ConstructionBuilderSize4 ()
		{
			key = "pipliz.baseresearch.constructionbuildersize4";
			icon = "gamedata/textures/icons/buildersize4.png";
			iterationCount = 100;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.constructionbuildersize3");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.builderlimit", 500000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
