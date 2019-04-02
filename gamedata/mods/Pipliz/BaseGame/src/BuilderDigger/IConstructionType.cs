using Jobs;
using NPC;

namespace Pipliz.Mods.BaseGame.Construction
{
	public interface IConstructionType
	{
		void DoJob (IIterationType iterationType, IAreaJob areaJob, ConstructionJobInstance job, ref NPCBase.NPCState state);
		int OnStockpileNewItemCount { get; }
	}
}
