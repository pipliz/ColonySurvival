using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagColony : BaseResearchable
	{
		public ScienceBagColony ()
		{
			key = "pipliz.baseresearch.sciencebagcolony";
			icon = "gamedata/textures/icons/sciencebagcolony.png";
			iterationCount = 5;
			AddIterationRequirement("bronzeanvil");
			AddIterationRequirement("fineryforge");
			AddIterationRequirement("matchlockgun");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.matchlockgun");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagcolony", true, "pipliz.technologist");
		}
	}
}
