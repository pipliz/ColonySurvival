using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
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
			AddDependency("pipliz.baseresearch.flaxfarming");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.tailorshop", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.tailorshop");
		}
	}
}
