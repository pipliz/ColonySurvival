using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionBuilderSize5 : BaseResearchable
	{
		public ConstructionBuilderSize5 ()
		{
			key = "pipliz.baseresearch.constructionbuildersize5";
			icon = "gamedata/textures/icons/buildersize5.png";
			iterationCount = 250;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.constructionbuildersize4");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.builderlimit", 1000000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
