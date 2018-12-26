using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction.Loaders
{
	public class DiggerSpecialLoader : IConstructionLoader
	{
		public string JobName { get { return "pipliz.diggerspecial"; } }

		public void ApplyTypes (ConstructionArea area, JSONNode node)
		{
			if (node != null) {
				ItemTypes.ItemType digTpe = ItemTypes.GetType(ItemTypes.IndexLookup.GetIndex(node.GetAsOrDefault("diggerBlockType", "air")));
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
