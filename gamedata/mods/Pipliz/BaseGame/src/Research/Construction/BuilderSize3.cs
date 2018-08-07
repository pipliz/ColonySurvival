
using Science;

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

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.builderlimit", 250000);
			ConstructionHelper.SendPacket(manager.Colony);
		}
	}
}
