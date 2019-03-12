using BlockEntities;
using BlockTypes;
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
			float cooldown = 1f;
			if (ItemTypes.TryGetType(belowType, out ItemTypes.ItemType foundtype)
				&& (foundtype.CustomDataNode?.TryGetAs("minerMiningTime", out cooldown) ?? false)
			) {
				BlockTypeBelow = foundtype;
				MiningCooldown = Random.NextFloat(0.9f, 1.1f) * cooldown;
			} else {
				// attempt to remove the job (loaded wrongly) - invoke on main thread to prevent nested entity callbacks (unsupported, deadlocks)
				ThreadManager.InvokeOnMainThread(() => ServerManager.TryChangeBlock(position, null, BuiltinBlocks.Types.air, Owner));
			}
		}

		public MinerJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony)
		{
			float cooldown = 1f;
			if (World.TryGetTypeAt(position.Add(0, -1, 0), out ItemTypes.ItemType itemIndex)
				&& (itemIndex.CustomDataNode?.TryGetAs("minerMiningTime", out cooldown) ?? false)
			) {
				BlockTypeBelow = itemIndex;
				MiningCooldown = Random.NextFloat(0.9f, 1.1f) * cooldown;
			} else {
				// attempt to remove the job (loaded wrongly) - invoke on main thread to prevent nested entity callbacks (unsupported, deadlocks)
				ThreadManager.InvokeOnMainThread(() => ServerManager.TryChangeBlock(position, null, BuiltinBlocks.Types.air, Owner));
			}
		}

		public override ESerializeEntityResult SerializeToBytes (Chunk chunk, Vector3Byte blockPosition, ByteBuilder builder)
		{
			var result = base.SerializeToBytes(chunk, blockPosition, builder);
			builder.WriteVariable(BlockTypeBelow?.ItemIndex ?? 0);
			return result;
		}
	}
}
