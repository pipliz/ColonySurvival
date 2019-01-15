using Jobs;
using NPC;

namespace Pipliz.Mods.BaseGame
{
	// blockjob but with some extra storage
	public class WaterGathererInstance : BlockJobInstance
	{
		public Vector3Int WaterPosition { get; set; } = Vector3Int.invalidPos;
		public int GatheredCount { get; set; }

		public WaterGathererInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader) { }
		public WaterGathererInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony) { }
	}
}
