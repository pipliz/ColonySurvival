using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class TailorShop : BaseResearchable
	{
		public TailorShop ()
		{
			key = "pipliz.baseresearch.tailorshop";
			icon = "gamedata/textures/icons/tailorshop.png";
			iterationCount = 3;
			AddIterationRequirement("coatedplanks");
			AddIterationRequirement("flax");
			AddIterationRequirement("coppertools");
			AddDependency("pipliz.baseresearch.bronzeanvil");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.tailorshop", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.tailorshop");
		}
	}
}
