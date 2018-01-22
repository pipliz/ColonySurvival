using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class QuarterBlockStonemason : BaseResearchable
	{
		public QuarterBlockStonemason ()
		{
			key = "pipliz.baseresearch.quarterblockstonemason";
			icon = "gamedata/textures/icons/quarterblockgrey.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.stonemasonworkbench");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			var storage = RecipeStorage.GetPlayerStorage(manager.Player);
			storage.SetRecipeAvailability("pipliz.stonemason.quarterblockgrey", true, "pipliz.stonemason");
		}
	}
}
