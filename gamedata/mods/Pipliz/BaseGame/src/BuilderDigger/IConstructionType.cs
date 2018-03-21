using NPC;

namespace Pipliz.Mods.BaseGame.Construction
{
	public interface IConstructionType
	{
		Shared.EAreaType AreaType { get; }
		Shared.EAreaMeshType AreaTypeMesh { get; }

		void DoJob (IIterationType iterationType, IAreaJob areaJob, ConstructionJob job, ref NPCBase.NPCState state);
	}
}
