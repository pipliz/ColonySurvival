using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class FlaxFarming : BaseResearchable
	{
		public FlaxFarming ()
		{
			key = "pipliz.baseresearch.flaxfarming";
			icon = "gamedata/textures/icons/flax.png";
			iterationCount = 3;
			AddIterationRequirement("berry", 3);
			AddIterationRequirement("coppernails");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.linseedoil", true, "pipliz.crafter");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.coatedplanks", true, "pipliz.crafter");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.adobe", true, "pipliz.crafter");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.bowstring", true, "pipliz.crafter");

			if (reason == EResearchCompletionReason.ProgressCompleted) {
				Stockpile.GetStockPile(manager.Player).Add(BlockTypes.Builtin.BuiltinBlocks.FlaxStage1, 100);
				if (manager.Player.IsConnected) {
					Chatting.Chat.Send(manager.Player, "You received 100 flax seeds!");
				}
			}
		}
	}
}
