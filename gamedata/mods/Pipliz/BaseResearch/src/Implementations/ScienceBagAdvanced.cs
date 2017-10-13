using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
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

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.technologist.sciencebagadvanced", true, "pipliz.technologist");
		}
	}
}
