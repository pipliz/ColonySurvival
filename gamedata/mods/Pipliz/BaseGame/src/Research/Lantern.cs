using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class Lantern : BaseResearchable
	{
		public Lantern ()
		{
			key = "pipliz.baseresearch.lantern";
			icon = "gamedata/textures/icons/lanternyellow.png";
			iterationCount = 5;
			AddIterationRequirement("sciencebagbasic", 5);
			AddIterationRequirement("ironrivet");
			AddIterationRequirement("crystal");
			AddDependency("pipliz.baseresearch.bloomery");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			var storage = RecipeStorage.GetPlayerStorage(manager.Player);
			storage.SetRecipeAvailability("pipliz.metalsmith.lanternyellow", true, "pipliz.metalsmith");
			storage.SetRecipeAvailability("pipliz.dyer.lanternwhite", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.lanterngreen", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.lanternblue", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.lanternred", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.lanternorange", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.lanterncyan", true, "pipliz.dyer");
			storage.SetRecipeAvailability("pipliz.dyer.lanternpink", true, "pipliz.dyer");

		}
	}
}
