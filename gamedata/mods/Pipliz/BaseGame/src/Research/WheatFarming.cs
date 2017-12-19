using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class WheatFarming : BaseResearchable
	{
		public WheatFarming ()
		{
			key = "pipliz.baseresearch.wheatfarming";
			icon = "gamedata/textures/icons/bread.png";
			iterationCount = 5;
			AddIterationRequirement("berry", 10);
			AddIterationRequirement("copperparts", 2);
			AddIterationRequirement("coppertools");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.oven", true, "pipliz.crafter");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.grindstone", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.oven");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.grindstone");

			if (reason == EResearchCompletionReason.ProgressCompleted) {
				Stockpile.GetStockPile(manager.Player).Add(BlockTypes.Builtin.BuiltinBlocks.WheatStage1, 400);
				if (manager.Player.IsConnected) {
					Chatting.Chat.Send(manager.Player, "You received 400 wheat seeds!");
				}
			}
		}
	}
}
