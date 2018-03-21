using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDiggerSize3 : BaseResearchable
	{
		public ConstructionDiggerSize3 ()
		{
			key = "pipliz.baseresearch.constructiondiggersize3";
			icon = "gamedata/textures/icons/diggersize3.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagadvanced");
			AddIterationRequirement("sciencebagcolony");
			AddDependency("pipliz.baseresearch.constructiondiggersize2");
			AddDependency("pipliz.baseresearch.sciencebagcolony");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			manager.Player.GetTempValues(true).Set("pipliz.diggerlimit", 250000);
			ConstructionHelper.SendPacket(manager.Player);
		}
	}
}
