
using Science;

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

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.diggerlimit", 25000);
			ConstructionHelper.SendPacket(manager.Colony);
		}
	}
}
