using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDiggerSize2 : BaseResearchable
	{
		public ConstructionDiggerSize2 ()
		{
			key = "pipliz.baseresearch.constructiondiggersize2";
			icon = "gamedata/textures/icons/diggersize2.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagadvanced");
			AddDependency("pipliz.baseresearch.constructiondiggersize1");
			AddDependency("pipliz.baseresearch.sciencebagadvanced");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.diggerlimit", 100000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
