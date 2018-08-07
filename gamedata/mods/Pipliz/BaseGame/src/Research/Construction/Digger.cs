
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDigger : BaseResearchable
	{
		public ConstructionDigger ()
		{
			key = "pipliz.baseresearch.constructiondigger";
			icon = "gamedata/textures/icons/bronzepickaxe.png";
			iterationCount = 30;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzepickaxe");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.diggerlimit", 5000);
			ConstructionHelper.SendPacket(manager.Colony);
		}
	}
}
