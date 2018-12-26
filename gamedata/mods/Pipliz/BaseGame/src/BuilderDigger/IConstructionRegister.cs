using Pipliz.JSON;

namespace Pipliz.Mods.BaseGame.Construction
{
	public interface IConstructionLoader
	{
		string JobName { get; }
		void ApplyTypes (ConstructionArea area, JSONNode node);
		void SaveTypes (ConstructionArea area, JSONNode node);
	}
}
