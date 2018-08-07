using Recipes;

namespace Pipliz.APIProvider.Jobs
{
	// blockjob but with some extra storage
	public class CraftingJobInstance : BlockJobInstance
	{
		public Recipe SelectedRecipe { get; set; }
		public int SelectedRecipeCount { get; set; }

		public CraftingJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader) { }

		public CraftingJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony) { }
	}
}
