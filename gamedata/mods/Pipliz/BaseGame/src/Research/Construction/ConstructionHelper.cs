using Shared.Networking;

namespace Pipliz.Mods.BaseGame.Researches
{
	[ModLoader.ModManager]
	public static class ConstructionHelper
	{
		public static void OnColonyChange (Players.Player player, Colony oldColony, Colony newColony)
		{
			if (player.ShouldSendData) {
				NetworkWrapper.Send(GetPacket(newColony), player);
			}
		}

		public static void SendPacket (Colony colony)
		{
			if (colony != null && colony.Owners.ShouldSendAny(colony)) {
				colony.Owners.SendToActive(colony, GetPacket(colony));
			}
		}

		public static byte[] GetPacket (Colony colony)
		{
			using (ByteBuilder b = ByteBuilder.Get()) {
				b.Write(ClientMessageType.ReceiveConstructionLimits);
				b.Write(colony?.TemporaryData.GetAsOrDefault("pipliz.builderlimit", 0) ?? 0);
				b.Write(colony?.TemporaryData.GetAsOrDefault("pipliz.diggerlimit", 0) ?? 0);
				return b.ToArray();
			}
		}
	}
}
