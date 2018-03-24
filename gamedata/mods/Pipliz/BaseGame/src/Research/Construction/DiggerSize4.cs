using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDiggerSize4 : BaseResearchable
	{
		public ConstructionDiggerSize4 ()
		{
			key = "pipliz.baseresearch.constructiondiggersize4";
			icon = "gamedata/textures/icons/diggersize4.png";
			iterationCount = 100;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.constructiondiggersize3");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.diggerlimit", 500000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
