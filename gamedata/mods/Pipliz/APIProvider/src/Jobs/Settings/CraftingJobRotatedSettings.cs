namespace Pipliz.APIProvider.Jobs
{
	public class CraftingJobRotatedSettings : CraftingJobSettings
	{
		public CraftingJobRotatedSettings (string blockType, string npcTypeKey, float craftingCooldown = 5f, int maxCraftsPerHaul = 5, string onCraftedAudio = null)
			: base(null, npcTypeKey, craftingCooldown, maxCraftsPerHaul, onCraftedAudio)
		{
			BlockTypes = new ItemTypes.ItemType[] {
				ItemTypes.GetType(blockType),
				ItemTypes.GetType(blockType + "x+"),
				ItemTypes.GetType(blockType + "x-"),
				ItemTypes.GetType(blockType + "z+"),
				ItemTypes.GetType(blockType + "z-")
			};
		}

		public override Vector3Int GetJobLocation (BlockJobInstance instance)
		{
			var position = instance.Position;
			if (BlockTypes.ContainsByReference(instance.BlockType, out int index)) {
				switch (index) {
					case 1:
						position.x += 1;
						break;
					case 2:
						position.x -= 1;
						break;
					case 3:
						position.z += 1;
						break;
					case 4:
						position.z -= 1;
						break;
				}
			}
			return position;
		}
	}
}
