using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class CrossBow : BaseResearchable
	{
		public CrossBow ()
		{
			key = "pipliz.baseresearch.crossbow";
			icon = "gamedata/textures/icons/crossbow.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.crossbowbolt");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.metalsmith.crossbow", true, "pipliz.metalsmith");
		}
	}
}
