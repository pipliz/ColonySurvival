using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionBuilderSize3 : BaseResearchable
	{
		public ConstructionBuilderSize3 ()
		{
			key = "pipliz.baseresearch.constructionbuildersize3";
			icon = "gamedata/textures/icons/buildersize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.constructionbuildersize2");
			AddDependency("pipliz.baseresearch.sciencebagcolony");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.builderlimit", 250000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
