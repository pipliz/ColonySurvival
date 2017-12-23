namespace Pipliz.Mods.APIProvider.Jobs
{
	public interface IBlockJobBase
	{
		ITrackableBlock InitializeOnAdd (Vector3Int position, ushort type, Players.Player player);
	}
}
