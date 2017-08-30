using Pipliz.APIProvider.Science;
using Server.Science;
using System.Collections.Generic;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class ScienceBagLife : BaseResearchable
	{
		public ScienceBagLife ()
		{
			key = "pipliz.baseresearch.sciencebaglife";
			icon = "baseresearch_sciencebaglife.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("bread");
			AddIterationRequirement("berry");
			AddDependency("pipliz.baseresearch.technologistbase");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			Recipe recipe = new Recipe(new List<InventoryItem>()
			{
				new InventoryItem ("flour"),
				new InventoryItem ("berry", 5),
				new InventoryItem ("clothing"),
				new InventoryItem ("linenbag")
			},
				new InventoryItem ("sciencebaglife")
			);
			RecipeStorage.GetPlayerStorage(manager.Player).AddAvailableRecipe("pipliz.technologist", recipe);
		}
	}
}
