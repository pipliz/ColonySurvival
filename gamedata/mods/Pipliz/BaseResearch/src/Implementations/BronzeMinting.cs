using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class BronzeMinting : BaseResearchable
	{
		public BronzeMinting ()
		{
			key = "pipliz.baseresearch.bronzeminting";
			icon = "gamedata/textures/icons/bronzecoin.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bronzeingot");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.mint", true, "pipliz.crafter");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.shop", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.mint");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.shop");
		}
	}
}
