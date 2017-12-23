using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ScienceBagAdvanced : BaseResearchable
	{
		public ScienceBagAdvanced ()
		{
			key = "pipliz.baseresearch.sciencebagadvanced";
			icon = "gamedata/textures/icons/sciencebagadvanced.png";
			iterationCount = 5;
			AddIterationRequirement("steelparts");
			AddIterationRequirement("gunpowderpouch");
			AddIterationRequirement("silveringot");
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.gunpowder");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagadvanced", true, "pipliz.technologist");
		}
	}
}
