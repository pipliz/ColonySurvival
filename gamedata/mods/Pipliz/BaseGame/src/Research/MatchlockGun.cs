using Pipliz.Mods.APIProvider.Science;
using Server.Science;

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

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.gunsmith.matchlockgun", true, "pipliz.gunsmith");
		}
	}
}
