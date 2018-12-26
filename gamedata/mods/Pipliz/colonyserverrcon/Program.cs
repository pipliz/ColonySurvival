using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RCONClient
{
	static class MainClass
	{
		static NetworkStream ServerStream;
		static TcpClient ServerConnection;
		static Queue<string> LogsToWrite = new Queue<string>();

		const byte IN_RCON_HEADER = 52; // mirrored in server/client message types
		const byte OUT_RCON_HEADER = 35; // mirrored in server/client message types

		const int PACKET_TYPE_PASSWORD = 0;
		const int PACKET_TYPE_CHAT = 1;
		const int PACKET_TYPE_QUIT = 2;
		const int PACKET_TYPE_PLAYERS = 3;

		const int IN_PACKET_PASS_ACCEPT = 0;
		const int IN_PACKET_PASS_DENY = 1;
		const int IN_PACKET_LOG = 2;

		struct LaunchArgs
		{
			public IPEndPoint IP;
			public string Password;
		}

		public static void Main (string[] args)
		{
			Console.Title = "Colony Survival RCON Client";
			Console.WriteLine("Launching Colony Survival RCON Client");

			if (args.Length > 0) {
				LaunchArgs LaunchArgs = new LaunchArgs();
				int i = 0;
				while (i < args.Length - 1) {
					if (string.Equals(args[i], "+server", StringComparison.Ordinal)) {
						int splitPos = args[i + 1].IndexOf(':');
						int port = int.Parse(args[i + 1].Substring(splitPos + 1, args[i + 1].Length - (splitPos + 1)));
						IPAddress ip = IPAddress.Parse(args[i + 1].Substring(0, splitPos));
						LaunchArgs.IP = new IPEndPoint(ip, port);
						i += 2;
					} else if (string.Equals(args[i], "+password", StringComparison.Ordinal)) {
						LaunchArgs.Password = args[i + 1];
						i += 2;
					} else {
						i++;
					}
				}

				if (LaunchArgs.IP != null) {
					StartServer(LaunchArgs);
				} else {
					ListHelp();
				}
			} else {
				ListHelp();
			}

			while (true) {
				if (Console.KeyAvailable) {
					ReadWriteLogs();
					Console.WriteLine("--Logging paused. Enter your command:");
					Console.Write("> ");
					if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {
						Console.ReadKey(true);
					}
					string read = Console.ReadLine();
					if (read != null) {
						read = read.TrimStart(' ', '\t', '\n', '\r');
					}
					if (string.IsNullOrEmpty(read)) {
						Console.WriteLine("--Logging continue");
						continue;
					}

					string start = read;
					string[] splits = null;
					if (read.Contains(" ")) {
						splits = read.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
						start = splits[0];
					}

					switch (start) {
						case "list":
						case "?":
						case "help":
							ListHelp();
							start = null;
							break;
					}

					if (start != null) {
						if (ServerConnection != null && ServerConnection.Connected) {
							switch (start) {
								case "quit_server":
									StopServer();
									break;
								case "chat":
									SendChat(read.Remove(0, "send ".Length));
									break;
								case "players":
									SendPlayers();
									break;
								default:
									Console.WriteLine("Unknown command");
									break;
							}
						} else {
							switch (start) {
								case "connect":
									StartServer(splits[1], splits[2]);
									break;
								default:
									Console.WriteLine("Unknown command");
									break;
							}
						}
					}

					Console.WriteLine("--Logging continue");
				}
				ReadWriteLogs();
				Thread.Sleep(50);
			}
		}

		static void ReadWriteLogs ()
		{
			if (ServerStream != null) {
				while (ServerStream.DataAvailable) {
					using (BinaryReader reader = new BinaryReader(ServerStream, System.Text.Encoding.Default, true)) {
						int byteLength = reader.ReadInt32();
						byte[] data = new byte[byteLength];
						int read = 0;
						while (read < byteLength) {
							int toRead = byteLength - read;
							read += ServerStream.Read(data, read, toRead);
						}
						using (MemoryStream bytes = new MemoryStream(data))
						using (BinaryReader packet = new BinaryReader(bytes)) {
							byte packetType = packet.ReadByte();
							if (packetType != IN_RCON_HEADER) {
								continue; // somehow got a wrong packet type - this is the reason we read all bytes of a message before parsing
							}
							int rconType = packet.ReadInt32();
							switch (rconType) {
								case IN_PACKET_LOG:
									LogsToWrite.Enqueue(packet.ReadString());
									break;
								case IN_PACKET_PASS_ACCEPT:
									LogsToWrite.Enqueue("Password accepted");
									break;
								case IN_PACKET_PASS_DENY:
									LogsToWrite.Enqueue("Password denied");
									break;
							}
						}
					}
				}
			}

			while (LogsToWrite.Count > 0) {
				Console.WriteLine(LogsToWrite.Dequeue());
			}
		}

		static void ListHelp ()
		{
			LogsToWrite.Enqueue("==== Available commands only if connected:");
			LogsToWrite.Enqueue("quit_server				- Asks the server to quit and disconnects.");
			LogsToWrite.Enqueue("chat {message}				- Sends a chat message.");
			LogsToWrite.Enqueue("players					- List connected players");
			LogsToWrite.Enqueue("==== Available commands only if not connected:");
			LogsToWrite.Enqueue("connect {ip:port} {password} - Will try to connect to the specified server");
			LogsToWrite.Enqueue("==== Available any time:");
			LogsToWrite.Enqueue("list,help,?				- Lists available commands");
			LogsToWrite.Enqueue("==== Example: connect 127.0.0.1:27004 mypw\nExample: chat hello world!");
		}

		static void StartServer (string ipport, string password)
		{
			int splitPos = ipport.IndexOf(':');
			int port = int.Parse(ipport.Substring(splitPos + 1, ipport.Length - (splitPos + 1)));
			IPAddress ip = IPAddress.Parse(ipport.Substring(0, splitPos));
			StartServer(new LaunchArgs()
			{
				IP = new IPEndPoint(ip, port),
				Password = password
			});
		}

		static void StartServer (LaunchArgs arg) {
			ServerConnection = new TcpClient();
			ServerConnection.Connect(arg.IP);
			ServerStream = ServerConnection.GetStream();
			if (arg.Password != null) {
				SendPassword(arg.Password);
			}
		}

		static void SendChat (string chat)
		{
			using (MemoryStream packet = new MemoryStream())
			using (BinaryWriter packetWriter = new BinaryWriter(packet)) {
				packetWriter.Write(0); // dummy length to overwrite later
				packetWriter.Write(OUT_RCON_HEADER);
				packetWriter.Write(PACKET_TYPE_CHAT);
				packetWriter.Write(chat);
				Send(packet, packetWriter);
			}
		}

		static void SendPassword (string password)
		{
			using (MemoryStream packet = new MemoryStream())
			using (BinaryWriter packetWriter = new BinaryWriter(packet)) {
				packetWriter.Write(0); // dummy length to overwrite later
				packetWriter.Write(OUT_RCON_HEADER);
				packetWriter.Write(PACKET_TYPE_PASSWORD);
				packetWriter.Write(password);
				Send(packet, packetWriter);
			}
		}

		static void SendPlayers ()
		{
			using (MemoryStream packet = new MemoryStream())
			using (BinaryWriter packetWriter = new BinaryWriter(packet)) {
				packetWriter.Write(0); // dummy length to overwrite later
				packetWriter.Write(OUT_RCON_HEADER);
				packetWriter.Write(PACKET_TYPE_PLAYERS);
				Send(packet, packetWriter);
			}
		}

		static void StopServer () {
			using (MemoryStream packet = new MemoryStream())
			using (BinaryWriter packetWriter = new BinaryWriter(packet)) {
				packetWriter.Write(0); // dummy length to overwrite later
				packetWriter.Write(OUT_RCON_HEADER);
				packetWriter.Write(PACKET_TYPE_QUIT);
				Send(packet, packetWriter);
			}

			ServerStream.Close();
			ServerStream = null;
			ServerConnection.Close();
			ServerConnection = null;
			LogsToWrite.Enqueue("Sent quit command, now disconnected");
		}

		static void Send (MemoryStream packet, BinaryWriter packetWriter)
		{
			long length = packet.Position;
			packetWriter.Seek(0, SeekOrigin.Begin);
			packetWriter.Write((int)(length - 4));
			packetWriter.Seek((int)length, SeekOrigin.Begin);

			byte[] array = packet.ToArray();
			ServerStream.Write(array, 0, array.Length);
		}
	}
}
