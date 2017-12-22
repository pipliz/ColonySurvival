using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class DyerTable : BaseResearchable
	{
		public DyerTable ()
		{
			key = "pipliz.baseresearch.dyertable";
			icon = "gamedata/textures/icons/dyertable.png";
			iterationCount = 10;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("hollyhock");
			AddDependency("pipliz.baseresearch.herbfarming");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.dyertable", true, "pipliz.crafter");
			RecipePlayer.UnlockOptionalRecipe(manager.Player, "pipliz.player.dyertable");
		}
	}
}
