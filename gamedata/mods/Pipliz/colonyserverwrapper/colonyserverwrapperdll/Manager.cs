using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Pipliz.ColonyServerWrapper
{
	[ModLoader.ModManager]
	public static class Manager
	{
		static TcpClient client;
		static NetworkStream clientStream;
		static BinaryReader clientReader;
		static BinaryWriter clientWriter;

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "pipliz.colonyserverwrapper.load")]
		[ModLoader.ModDocumentation("Checks commandline args, sets up connection if required")]
		static void OnLoad (string path) {
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 1; i < args.Length; i++) {
				if (args[i] == "+logto") {
					if (int.TryParse(args[i + 1], out int port)) {
						StartSendingLogsTo(port);
					} else {
						Log.Write($"Could not parse {args[i + 1]} as port");
					}
					return;
				}
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnUpdate, "pipliz.colonyserverwrapper.process")]
		[ModLoader.ModDocumentation("Processes packets from external socket")]
		static void Process ()
		{
			if (client != null) {
				while (client.Available >= 1) {
					string packetType = clientReader.ReadString();
					switch (packetType) {
						case "chat":
							Chatting.Chat.ReceiveServer(clientReader.ReadString());
							break;
						case "quit":
							Application.QueueKill();
							break;
					}
				}
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnQuitLate, "pipliz.colonyserverwrapper.dispose")]
		[ModLoader.ModCallbackDependsOn("pipliz.shared.waitforasyncquits")]
		[ModLoader.ModDocumentation("Close external socket")]
		static void Quit ()
		{
			if (client != null) {
				client.Close();
			}
			if (clientStream != null) {
				clientStream.Dispose();
				clientStream = null;
			}
			clientReader = null;
			clientWriter = null;
		}

		static void StartSendingLogsTo (int port)
		{
			Log.Write("Setting up connection to send logs to port {0}", port);
			client = new TcpClient();
			client.Connect(IPAddress.Loopback, port);
			clientStream = client.GetStream();
			clientReader = new BinaryReader(clientStream, System.Text.Encoding.UTF8);
			clientWriter = new BinaryWriter(clientStream, System.Text.Encoding.UTF8);

			UnityEngine.Application.logMessageReceived += SendLogMessage;
		}

		static void SendLogMessage (string condition, string stackTrace, UnityEngine.LogType type)
		{
			if (client != null && client.Connected) {
				clientWriter.Write("log");
				clientWriter.Write(condition);
			}
		}
	}
}
