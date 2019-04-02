using BlockEntities;
using Jobs;

namespace Pipliz.Mods.BaseGame.Construction
{
	// blockjob but with some extra storage
	public class ConstructionJobInstance : BlockJobInstance
	{
		public int StoredItemCount { get; set; }
		public ConstructionArea ConstructionArea { get; set; }
		public bool DidAreaPresenceTest { get; set; }

		public ConstructionJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader)
		{
			StoredItemCount = reader.ReadVariableInt();
		}

		public ConstructionJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony)
		{
		}

		public override ESerializeEntityResult SerializeToBytes (Chunk chunk, Vector3Byte blockPosition, ByteBuilder builder)
		{
			var result = base.SerializeToBytes(chunk, blockPosition, builder);
			builder.WriteVariable(StoredItemCount);
			return result;
		}

		public virtual void OnNPCAtConstructionStockpile ()
		{
			StoredItemCount = ConstructionArea?.ConstructionType?.OnStockpileNewItemCount ?? 5;
		}
	}
}
