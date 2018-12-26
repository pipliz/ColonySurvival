using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction.Loaders
{
    public class BuilderLoader : IConstructionLoader
    {
        public string JobName { get { return "pipliz.builder"; } }

		public void ApplyTypes (ConstructionArea area, JSONNode node)
		{
			if (node != null) {
				ItemTypes.ItemType buildType = ItemTypes.GetType(ItemTypes.IndexLookup.GetIndex(node.GetAsOrDefault("builderBlockType", "air")));
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
