using NPC;

namespace Pipliz.APIProvider.Jobs
{
	// blockjob but with some extra storage
	public class GuardJobInstance : BlockJobInstance
	{
		public IMonster Target { get; set; }
		public bool HasTarget { get { return Target != null && Target.IsValid; } }

		public GuardJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, ByteReader reader) : base(settings, position, type, reader) { }

		public GuardJobInstance (IBlockJobSettings settings, Vector3Int position, ItemTypes.ItemType type, Colony colony) : base(settings, position, type, colony) { }
	}
}
