namespace Pipliz.Mods.APIProvider.Jobs
{
	public interface IBlockJobManager
	{
		void RegisterCallback ();
		void Load ();
		void OnSave ();
		void OnRemove (Vector3Int position, ushort type, Players.Player player);
		void OnAdd (Vector3Int position, ushort type, Players.Player player);
	}
}
