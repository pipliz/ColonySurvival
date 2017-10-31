using Pipliz.JSON;

namespace Pipliz.APIProvider.Jobs
{
	public class RotatedCraftingJobBase : CraftingJobBase, IRecipeLimitsProvider
	{
		protected ushort blockType;
		protected Vector3Int PreferedNPCPosition;
		
		public override ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player)
		{
			blockType = type;
			PreferedNPCPosition = GetPositionNPC(position);
			return base.InitializeOnAdd(position, type, player);
		}

		public override ITrackableBlock InitializeFromJSON (Players.Player player, JSONNode node)
		{
			blockType = ItemTypes.IndexLookup.GetOrGenerate(node.GetAs<string>("type"));
			position = (Vector3Int)node["position"];
			PreferedNPCPosition = GetPositionNPC(position);
			return base.InitializeFromJSON(player, node);
		}

		public override Vector3Int GetJobLocation ()
		{
			return PreferedNPCPosition;
		}

		public virtual Vector3Int GetPositionNPC (Vector3Int position) { throw new System.NotImplementedException(); }

		public override JSONNode GetJSON ()
		{
			return base.GetJSON()
				.SetAs("type", ItemTypes.IndexLookup.GetName(blockType));
		}
	}
}
