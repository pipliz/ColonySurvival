using BlockEntities;
using Pipliz.APIProvider.Jobs;

namespace Pipliz.Mods.BaseGame
{
	// blockjob but with some extra storage
	public class MinerJobInstance : BlockJobInstance
	{
		public float MiningCooldown { get; set; }
		public ItemTypes.ItemType BlockTypeBelow { get; set; }

		public MinerJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader)
		{
			ushort belowType = reader.ReadVariableUShort();
			float cooldown = 1f;
			if (ItemTypes.TryGetType(belowType, out ItemTypes.ItemType foundtype)
				&& (foundtype.CustomDataNode?.TryGetAs("minerMiningTime", out cooldown) ?? false)
			) {
				BlockTypeBelow = foundtype;
				MiningCooldown = cooldown;
			} else {
				// attempt to remove the job (loaded wrongly)
				// todo use colony as param
				ThreadManager.InvokeOnMainThread(() => ServerManager.TryChangeBlock(position, 0, null));
			}
		}

		public MinerJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony)
		{
			float cooldown = 1f;
			if (World.TryGetTypeAt(position.Add(0, -1, 0), out ushort itemIndex)
				&& ItemTypes.TryGetType(itemIndex, out ItemTypes.ItemType foundtype)
				&& (foundtype.CustomDataNode?.TryGetAs("minerMiningTime", out cooldown) ?? false)
			) {
				BlockTypeBelow = foundtype;
				MiningCooldown = cooldown;
			} else {
				// todo use colony as param
				ThreadManager.InvokeOnMainThread(() => ServerManager.TryChangeBlock(position, 0, null));
			}
		}

		public override ESerializeEntityResult SerializeToBytes (Vector3Int blockPosition, ByteBuilder builder)
		{
			var result = base.SerializeToBytes(blockPosition, builder);
			builder.WriteVariable(BlockTypeBelow?.ItemIndex ?? 0);
			return result;
		}
	}
}
