using Pipliz.Mods.APIProvider.Science;
using Server.Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class Archery : BaseResearchable
	{
		public Archery ()
		{
			key = "pipliz.baseresearch.archery";
			icon = "gamedata/textures/icons/bow.png";
			iterationCount = 3;
			AddIterationRequirement("bronzearrow", 3);
			AddIterationRequirement("bowstring");
			AddDependency("pipliz.baseresearch.bronzeanvil");
			AddDependency("pipliz.baseresearch.flaxfarming");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			RecipeStorage.GetPlayerStorage(manager.Player).SetRecipeAvailability("pipliz.crafter.bow", true, "pipliz.crafter");
		}
	}
}
