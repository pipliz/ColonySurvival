namespace Pipliz.APIProvider.Jobs
{
	public interface IBlockJobManager
	{
		void RegisterCallback ();
		void Load ();
		void OnSave ();
	}
}
