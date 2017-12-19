using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagBasic : BaseResearchable
	{
		public ScienceBagBasic ()
		{
			key = "pipliz.baseresearch.sciencebagbasic";
			icon = "gamedata/textures/icons/sciencebagbasic.png";
			iterationCount = 3;
			AddIterationRequirement("coppertools");
			AddIterationRequirement("bronzeplate");
			AddIterationRequirement("bricks", 3);
			AddDependency("pipliz.baseresearch.technologisttable");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagbasic", true, "pipliz.technologist");
		}
	}
}
