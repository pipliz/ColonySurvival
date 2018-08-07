
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDiggerSize5 : BaseResearchable
	{
		public ConstructionDiggerSize5 ()
		{
			key = "pipliz.baseresearch.constructiondiggersize5";
			icon = "gamedata/textures/icons/diggersize5.png";
			iterationCount = 250;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.constructiondiggersize4");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			manager.Colony.TemporaryData.SetAs("pipliz.diggerlimit", 1000000);
			ConstructionHelper.SendPacket(manager.Colony);
		}
	}
}
