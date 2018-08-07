using Science;

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

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.builderlimit", 25000);
			ConstructionHelper.SendPacket(manager.Colony);
		}
	}
}
