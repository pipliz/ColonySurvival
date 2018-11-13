using Recipes;
using Science;

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

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				
				manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.AlkanetStage1, 100);
				manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.HollyhockStage1, 100);
				manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.WolfsbaneStage1, 100);
				foreach (var owner in manager.Colony.Owners) {
					if (owner.ShouldSendData) {
						Chatting.Chat.Send(owner, "You received 100 Alkanet, Hollyhock and Wolfsbane seeds!");
					}
				}
			}

			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.merchant.alkanetseeds"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.merchant.hollyhockseeds"));
			recipeData.UnlockRecipe(new RecipeKey("pipliz.merchant.wolfsbaneseeds"));
		}
	}
}
