using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class GunPowder : BaseResearchable
	{
		public GunPowder ()
		{
			key = "pipliz.baseresearch.gunpowder";
			icon = "gamedata/textures/icons/gunpowder.png";
			iterationCount = 25;
			AddIterationRequirement("salpeter");
			AddIterationRequirement("sciencebagmilitary");
			AddDependency("pipliz.baseresearch.gunsmithshop");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.gunsmith.gunpowder", true, "pipliz.gunsmith");
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.gunsmith.gunpowderpouch", true, "pipliz.gunsmith");
		}
	}
}
