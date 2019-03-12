using BlockEntities;
using Jobs;

namespace Pipliz.Mods.BaseGame
{
	// blockjob but with some extra storage
	public class ScientistJobInstance : BlockJobInstance
	{
		public int StoredItemCount { get; set; }

		public ScientistJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader)
		{
			if (reader == null || reader.AtEnd) {
				StoredItemCount = 0;
			} else {
				StoredItemCount = reader.ReadVariableInt();
			}
		}

		public ScientistJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony)
		{

		}

		public override ESerializeEntityResult SerializeToBytes (Chunk chunk, Vector3Byte blockPosition, ByteBuilder builder)
		{
			var result = base.SerializeToBytes(chunk, blockPosition, builder);
			builder.WriteVariable(StoredItemCount);
			return result;
		}
	}
}
