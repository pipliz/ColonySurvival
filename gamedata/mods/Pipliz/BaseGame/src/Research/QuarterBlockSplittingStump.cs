using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class QuarterBlockSplittingStump : BaseResearchable
	{
		public QuarterBlockSplittingStump ()
		{
			key = "pipliz.baseresearch.quarterblocksplittingstump";
			//icon = "gamedata/textures/icons/quarterblock.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.splittingstump");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			var storage = RecipeStorage.GetPlayerStorage(manager.Player);
			storage.SetRecipeAvailability("pipliz.dyer.quarterblockbrowndark", true, "pipliz.woodcutter");
			storage.SetRecipeAvailability("pipliz.dyer.quarterblockbrownlight", true, "pipliz.woodcutter");
		}
	}
}
