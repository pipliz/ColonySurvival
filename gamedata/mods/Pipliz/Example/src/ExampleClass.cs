namespace ExampleMod
{
	[ModLoader.ModManager]
	public class ExampleClass
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterAssemblyLoad)]
		public static void AfterAssemblyLoad ()
		{
			Pipliz.Log.Write("Hello from ExampleClass.AfterAssemblyLoad");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined)]
		public static void AfterItemTypesDefined ()
		{
			Pipliz.Log.Write("Hello from ExampleClass.AfterItemTypesDefined");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterNetworkSetup)]
		public static void AfterNetworkSetup ()
		{
			Pipliz.Log.Write("Hello from ExampleClass.AfterNetworkSetup");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterSelectedWorld)]
		public static void AfterSelectedWorld ()
		{
			Pipliz.Log.Write("Hello from ExampleClass.AfterSelectedWorld");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterStartup)]
		public static void AfterStartup ()
		{
			Pipliz.Log.Write("Hello from ExampleClass.AfterStartup");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterWorldLoad)]
		public static void AfterWorldLoad ()
		{
			Pipliz.Log.Write("Hello from ExampleClass.AfterWorldLoad");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnApplicationFocus)]
		public static void OnApplicationFocus (bool state)
		{
			Pipliz.Log.Write("Hello from ExampleClass.OnApplicationFocus");
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnApplicationPause)]
		public static void OnApplicationPause (bool state)
		{
			Pipliz.Log.Write("Hello from ExampleClass.OnApplicationPause");
		}

		static int fixedUpdateCount = 0;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnFixedUpdate)]
		public static void OnFixedUpdate ()
		{
			if (fixedUpdateCount++ % 200 == 0) {
				Pipliz.Log.Write("Hello from ExampleClass.OnFixedUpdate");
			}
		}

		static int lateUpdateCount = 0;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnLateUpdate)]
		public static void OnLateUpdate ()
		{
			if (lateUpdateCount++ % 450 == 0) {
				Pipliz.Log.Write("Hello from ExampleClass.OnLateUpdate");
			}
		}

		static int updateCount = 0;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnUpdate)]
		public static void OnUpdate ()
		{
			if (updateCount++ % 500 == 0) {
				Pipliz.Log.Write("Hello from ExampleClass.OnUpdate");
			}
		}

		static int updateEndCount = 0;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnUpdateEnd)]
		public static void OnUpdateEnd ()
		{
			if (updateEndCount++ % 500 == 0) {
				Pipliz.Log.Write("Hello from ExampleClass.OnUpdateEnd");
			}
		}

		static int updateStartCount = 0;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnUpdateStart)]
		public static void OnUpdateStart ()
		{
			if (updateStartCount++ % 500 == 0) {
				Pipliz.Log.Write("Hello from ExampleClass.OnUpdateStart");
			}
		}
	}
}
