
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionBuilder : BaseResearchable
	{
		public ConstructionBuilder ()
		{
			key = "pipliz.baseresearch.constructionbuilder";
			icon = "gamedata/textures/icons/bricksred.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("stoneblock", 5);
			AddIterationRequirement("cobblestonegrey", 5);
			AddIterationRequirement("bricksred", 5);
			AddDependency("pipliz.baseresearch.constructiondigger");
			AddDependency("pipliz.baseresearch.stonemasonworkbench");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.builderlimit", 1000);
			ConstructionHelper.SendPacket(manager.Colony);
		}
	}
}
