namespace Pipliz.APIProvider.Jobs
{
	public class CraftingJobRotatedLitSettings : CraftingJobSettings
	{
		public CraftingJobRotatedLitSettings (string blockType, string npcTypeKey, float craftingCooldown = 5f, int maxCraftsPerHaul = 5, string onCraftedAudio = null)
			: base(null, npcTypeKey, craftingCooldown, maxCraftsPerHaul, onCraftedAudio)
		{
			BlockTypes = new ItemTypes.ItemType[] {
				ItemTypes.GetType(blockType),
				ItemTypes.GetType(blockType + "x+"),
				ItemTypes.GetType(blockType + "x-"),
				ItemTypes.GetType(blockType + "z+"),
				ItemTypes.GetType(blockType + "z-"),
				ItemTypes.GetType(blockType + "lit"),
				ItemTypes.GetType(blockType + "litx+"),
				ItemTypes.GetType(blockType + "litx-"),
				ItemTypes.GetType(blockType + "litz+"),
				ItemTypes.GetType(blockType + "litz-")
			};
		}

		public override Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			var position = instance.Position;
			if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
				switch (index) {
					case 1:
					case 6:
						position.x += 1;
						break;
					case 2:
					case 7:
						position.x -= 1;
						break;
					case 3:
					case 8:
						position.z += 1;
						break;
					case 4:
					case 9:
						position.z -= 1;
						break;
				}
			}
			return position;
		}

		public override void OnStartCrafting (BlockJobInstance instance)
		{
			if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
				switch (index) {
					case 1:
					case 2:
					case 3:
					case 4:
						ItemTypes.ItemType newType = BlockTypes[index + 5];
						// use colony as cause param
						if (ServerManager.TryChangeBlock(instance.Position, newType.ItemIndex, null, ServerManager.SetBlockFlags.None)) {
							instance.BlockType = newType;
						}
						break;
				}
			}
		}

		public override void OnStopCrafting (BlockJobInstance instance)
		{
			if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
				switch (index) {
					case 6:
					case 7:
					case 8:
					case 9:
						ItemTypes.ItemType newType = BlockTypes[index - 5];
						// use colony as cause param
						if (ServerManager.TryChangeBlock(instance.Position, newType.ItemIndex, null, ServerManager.SetBlockFlags.None)) {
							instance.BlockType = newType;
						}
						break;
				}
			}
		}
	}
}
