using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagMilitary : BaseResearchable
	{
		public ScienceBagMilitary ()
		{
			key = "pipliz.baseresearch.sciencebagmilitary";
			icon = "gamedata/textures/icons/sciencebagmilitary.png";
			iterationCount = 3;
			AddIterationRequirement("ironsword");
			AddIterationRequirement("bronzearrow", 3);
			AddIterationRequirement("bow");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.technologisttable");
			AddDependency("pipliz.baseresearch.bloomery");
			AddDependency("pipliz.baseresearch.archery");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagmilitary", true, "pipliz.technologist");
		}
	}
}
