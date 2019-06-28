using BlockEntities;
using Jobs;

namespace Pipliz.Mods.BaseGame
{
	// blockjob but with some extra storage
	public class MinerJobInstance : BlockJobInstance
	{
		public float MiningCooldown { get; set; }
		public ItemTypes.ItemType BlockTypeBelow { get; set; }
		public int GatheredItemCount { get; set; }

		public MinerJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader)
		{
			ushort belowType = reader.ReadVariableUShort();
			MiningCooldown = -1f;
			if (ItemTypes.TryGetType(belowType, out ItemTypes.ItemType foundtype)) {
				BlockTypeBelow = foundtype;
			}
		}

		public MinerJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony)
		{
			BlockTypeBelow = null;
			MiningCooldown = -1f;
		}

		public override ESerializeEntityResult SerializeToBytes (Chunk chunk, Vector3Byte blockPosition, ByteBuilder builder)
		{
			var result = base.SerializeToBytes(chunk, blockPosition, builder);
			builder.WriteVariable(BlockTypeBelow?.ItemIndex ?? 0);
			return result;
		}
	}
}
