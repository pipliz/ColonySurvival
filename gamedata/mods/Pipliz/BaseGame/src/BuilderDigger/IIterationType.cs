namespace Pipliz.Mods.BaseGame.Construction
{
	public interface IIterationType
	{
		Vector3Int CurrentPosition { get; }
		bool MoveNext ();
	}
}
