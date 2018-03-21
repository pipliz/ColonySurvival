namespace Pipliz.Mods.BaseGame.Researches
{
	[ModLoader.ModManager]
	public static class ConstructionHelper
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerConnectedLate, "pipliz.mods.basegame.sendconstructiondata")]
		public static void SendPacket (Players.Player player)
		{
			if (player.IsConnected) {
				int builder = player.GetTempValues(false).GetOrDefault("pipliz.builderlimit", 0);
				int digger = player.GetTempValues(false).GetOrDefault("pipliz.diggerlimit", 0);

				using (ByteBuilder b = ByteBuilder.Get()) {
					b.Write(General.Networking.ClientMessageType.ReceiveConstructionLimits);
					b.Write(builder);
					b.Write(digger);
					NetworkWrapper.Send(b.ToArray(), player);
				}
			}
		}
	}
}
