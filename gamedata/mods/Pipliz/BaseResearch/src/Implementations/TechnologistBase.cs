using Pipliz.APIProvider.Science;
using Server.Science;
using System.Collections.Generic;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class TechnologistBase : BaseResearchable
	{
		public TechnologistBase ()
		{
			key = "pipliz.baseresearch.technologistbase";
			icon = "baseresearch_technologistbase.png";
			iterationCount = 15;
			AddIterationRequirement("linenbag");
			AddDependency("pipliz.baseresearch.tailorbase");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			Recipe recipe = new Recipe(new List<InventoryItem>()
			{
				new InventoryItem ("coatedplanks"),
				new InventoryItem ("ironingot"),
				new InventoryItem ("linenbag")
			},
				new InventoryItem ("technologisttable")
			);
			RecipeStorage.GetPlayerStorage(manager.Player).AddAvailableRecipe("pipliz.crafter", recipe);
			RecipePlayer.AddUnlockedRecipe(manager.Player, recipe);
		}
	}
}
