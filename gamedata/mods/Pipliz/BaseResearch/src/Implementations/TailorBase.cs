using Pipliz.APIProvider.Science;
using Server.Science;
using System.Collections.Generic;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class TailorBase : BaseResearchable
	{
		public TailorBase ()
		{
			key = "pipliz.baseresearch.tailorbase";
			icon = "baseresearch_tailorbase.png";
			iterationCount = 15;
			AddIterationRequirement("flax");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			Recipe recipe = new Recipe(new List<InventoryItem>()
			{
				new InventoryItem ("planks"),
				new InventoryItem ("ironingot"),
				new InventoryItem ("flax")
			},
				new InventoryItem ("tailorshop")
			);
			RecipeStorage.GetPlayerStorage(manager.Player).AddAvailableRecipe("pipliz.crafter", recipe);
			RecipePlayer.AddUnlockedRecipe(manager.Player, recipe);
		}
	}
}
