namespace Pipliz.Mods.BaseGame.Construction
{
	public interface IConstructionType
	{
		Shared.EAreaType AreaType { get; }
		Shared.EAreaMeshType AreaTypeMesh { get; }

		void DoJob (IIterationType iterationType, IAreaJob job, ref NPC.NPCBase.NPCState state);
	}
}
