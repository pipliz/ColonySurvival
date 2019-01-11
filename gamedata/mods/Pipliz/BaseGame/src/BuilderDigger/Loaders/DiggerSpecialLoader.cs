using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction.Loaders
{
	public class DiggerSpecialLoader : IConstructionLoader
	{
		public string JobName { get { return "pipliz.diggerspecial"; } }

		public void ApplyTypes (ConstructionArea area, JSONNode node)
		{
			JSONNode arr;
			if (node != null && node.TryGetChild("selectedTypes", out arr) && arr.NodeType == NodeType.Array && arr.ChildCount > 0) {
				ItemTypes.ItemType digTpe = ItemTypes.GetType(ItemTypes.IndexLookup.GetIndex(arr[0].GetAs<string>()));
				if (digTpe != null && digTpe.ItemIndex != 0) {
					area.ConstructionType = new Types.DiggerSpecial(digTpe);
					area.IterationType = new Iterators.TopToBottom(area);
				}
			}
		}

		public void SaveTypes (ConstructionArea area, JSONNode node)
		{
			// todo in the future - save the iterator progress here
		}
	}
}
