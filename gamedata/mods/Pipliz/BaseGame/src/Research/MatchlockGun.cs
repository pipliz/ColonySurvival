using Recipes;
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class MatchlockGun : BaseResearchable
	{
		public MatchlockGun ()
		{
			key = "pipliz.baseresearch.matchlockgun";
			icon = "gamedata/textures/icons/matchlockgun.png";
			iterationCount = 25;
			AddIterationRequirement("leadbullet");
			AddIterationRequirement("gunpowderpouch");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.gunpowder");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
			var recipeData = manager.Colony.RecipeData;
			recipeData.UnlockRecipe(new RecipeKey("pipliz.gunsmith.matchlockgun"));
		}
	}
}
