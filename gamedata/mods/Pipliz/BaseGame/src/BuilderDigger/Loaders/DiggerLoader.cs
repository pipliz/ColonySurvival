using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction.Loaders
{
    public class DiggerLoader : IConstructionLoader
    {
        public string JobName { get { return "pipliz.digger"; } }

		public void ApplyTypes (ConstructionArea area, JSONNode node)
		{
			area.ConstructionType = new Types.DiggerBasic();
			area.IterationType = new Iterators.TopToBottom(area);
		}

		public void SaveTypes (ConstructionArea area, JSONNode node)
		{
			// todo in the future - save the iterator progress here
		}
	}
}
