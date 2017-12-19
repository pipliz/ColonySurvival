namespace Pipliz.Mods.APIProvider.Jobs
{
	public interface IBlockJobManager
	{
		void RegisterCallback ();
		void Load ();
		void OnSave ();
	}
}
