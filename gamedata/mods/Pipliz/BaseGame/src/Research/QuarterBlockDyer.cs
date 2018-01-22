using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class QuarterBlockDyer : BaseResearchable
	{
		public QuarterBlockDyer ()
		{
			key = "pipliz.baseresearch.quarterblockdyer";
			icon = "gamedata/textures/icons/quarterblockwhite.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("gypsum");
			AddIterationRequirement("charcoal");
			AddDependency("pipliz.baseresearch.dyertable");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			var storage = RecipeStorage.GetPlayerStorage(manager.Player);
			storage.SetRecipeAvailability("pipliz.dyer.quarterblockblack", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.quarterblockwhite", true, "pipliz.dyer");
		}
	}
}
