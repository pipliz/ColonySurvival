using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction.Loaders
{
    public class BuilderLoader : IConstructionLoader
    {
        public string JobName { get { return "pipliz.builder"; } }

		public void ApplyTypes (ConstructionArea area, JSONNode node)
		{
			JSONNode arr;
			if (node != null && node.TryGetChild("selectedTypes", out arr) && arr.NodeType == NodeType.Array && arr.ChildCount > 0) {
				ItemTypes.ItemType buildType = ItemTypes.GetType(ItemTypes.IndexLookup.GetIndex(arr[0].GetAs<string>()));
				if (buildType != null && buildType.ItemIndex != 0) {
					area.ConstructionType = new Types.BuilderBasic(buildType);
					area.IterationType = new Iterators.BottomToTop(area);
				}
			}
		}

		public void SaveTypes (ConstructionArea area, JSONNode node)
		{
			// todo in the future - save the iterator progress here
		}
	}
}
