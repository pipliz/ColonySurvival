using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class FineryForge : BaseResearchable
	{
		public FineryForge ()
		{
			key = "pipliz.baseresearch.fineryforge";
			icon = "gamedata/textures/icons/fineryforge.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("ironwrought");
			AddDependency("pipliz.baseresearch.bloomery");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.metalsmith.fineryforge", true, "pipliz.metalsmith");
		}
	}
}
