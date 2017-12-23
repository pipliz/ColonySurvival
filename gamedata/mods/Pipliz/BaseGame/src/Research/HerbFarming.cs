using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class HerbFarming : BaseResearchable
	{
		public HerbFarming ()
		{
			key = "pipliz.baseresearch.herbfarming";
			icon = "gamedata/textures/icons/alkanet.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddDependency("pipliz.baseresearch.sciencebagbasic");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				Stockpile.GetStockPile(manager.Player).Add(BlockTypes.Builtin.BuiltinBlocks.AlkanetStage1, 100);
				Stockpile.GetStockPile(manager.Player).Add(BlockTypes.Builtin.BuiltinBlocks.HollyhockStage1, 100);
				Stockpile.GetStockPile(manager.Player).Add(BlockTypes.Builtin.BuiltinBlocks.WolfsbaneStage1, 100);
				if (manager.Player.IsConnected) {
					Chatting.Chat.Send(manager.Player, "You received 100 Alkanet, Hollyhock and Wolfsbane seeds!");
				}
			}
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.merchant.alkanetseeds", true, "pipliz.merchant");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.merchant.hollyhockseeds", true, "pipliz.merchant");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.merchant.wolfsbaneseeds", true, "pipliz.merchant");
		}
	}
}
