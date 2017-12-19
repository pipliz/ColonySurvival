using Server.GrowableBlocks;

namespace Pipliz.Mods.APIProvider.GrowableBlocks
{
	/// <summary>
	/// Builtin IGrowableStage implementation. Nothing exciting
	/// </summary>
	public class GrowableStage : IGrowableStage
	{
		string blockType;
		float growthTime;
		ushort blockTypeIndex;

		public GrowableStage (string blockType = null, float growthTime = 0f)
		{
			this.blockType = blockType;
			this.growthTime = growthTime;
			if (blockType != null) {
				blockTypeIndex = ItemTypes.IndexLookup.GetIndex(blockType);
			}
		}

		public string BlockType { get { return blockType; } }

		public ushort BlockTypeIndex { get { return blockTypeIndex; } }

		public float GrowthTime { get { return growthTime; } }
	}
}
