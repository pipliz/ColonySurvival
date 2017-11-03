namespace Pipliz.ColonyServerWrapper
{
	[ModLoader.ModManager]
	public static class Manager
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "pipliz.colonyserverwrapper.load")]
		static void OnLoad (string path) {

		}
	}
}
