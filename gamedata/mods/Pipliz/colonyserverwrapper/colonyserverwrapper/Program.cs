using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace ColonyServerWrapper
{
	static class MainClass
	{
		static Process ServerProcess = null;
		static TcpListener ServerListener = null;
		static Thread LoggingThread = null;
		static List<KeyValuePair<string, string>> MessagesToSend = new List<KeyValuePair<string, string>>();
		static Queue<string> QueuedLogs = new Queue<string>();

		static bool IsServerRunning {
			get {
				if (ServerProcess == null) {
					return false;
				}
				if (ServerProcess.HasExited) {
					ServerProcess = null;
					return false;
				}
				return true;
			}
		}

		static void CheckDllFile (string source, string target)
		{
			try {
				if (File.Exists(source)) {
					WriteConsole("Copying {0} to {1}", source, target);
					File.Copy(source, target, true);
				}
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}

		public static void Main (string[] args)
		{
			Console.Title = "Colony Survival Dedicated Server";
			WriteConsole("Launching Colony Survival Dedicated Server");

			FixWorkingDirectory();
			
			CheckDllFile("linux32/steamclient.so", "colonyserver_Data/Plugins/x86/steamclient.so");
			CheckDllFile("linux64/steamclient.so", "colonyserver_Data/Plugins/x86_64/steamclient.so");

			Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
			{
				if (IsServerRunning) {
					StopServer();
				}
			};

			if (args.Length > 0) {
				StringBuilder argsBuilder = new StringBuilder();
				for (int i = 0; i < args.Length; i++) {
					string arg = args[i];
					if (arg.Contains(" ")) {
						argsBuilder.AppendFormat("\"{0}\" ", arg);
					} else {
						argsBuilder.AppendFormat("{0} ", arg);
					}
				}
				argsBuilder.AppendFormat(" +parentprocess {0} ", Process.GetCurrentProcess().Id);
				string fullArgs = argsBuilder.ToString();
				WriteConsole("Launching server from commandline args: {0}", fullArgs);
				StartServer(fullArgs);
			} else {
				ListHelp();
			}

			while (true) {
				if (Console.KeyAvailable) {
					PrintQueuedLogs();
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
					if (read.Contains(" ")) {
						start = read.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
					}
					switch (start) {
						case "quit":
							if (IsServerRunning) {
								StopServer();
							}
							return;
						case "help":
						case "list":
						case "?":
							ListHelp();
							break;
						case "start_server":
							if (read.Length < "start_server  ".Length) {
								goto case "help";
							}
							StringBuilder str = new StringBuilder();
							str.Append(read, "start_server ".Length, read.Length - "start_server ".Length);
							str.AppendFormat(" +parentprocess {0} ", Process.GetCurrentProcess().Id);
							StartServer(str.ToString());
							break;
						case "stop_server":
							StopServer();
							break;
						case "send":
							if (read.Length < "send  ".Length) {
								goto case "help";
							}
							SendLog(read.Remove(0, "send ".Length));
							break;
						default:
							Console.WriteLine("Unknown command");
							break;
					}
					Console.WriteLine("--Logging continue");
				}
				PrintQueuedLogs();
				Thread.Sleep(50);
			}
		}

		static void PrintQueuedLogs ()
		{
			if (QueuedLogs.Count > 0) {
				lock (QueuedLogs) {
					while (QueuedLogs.Count > 0) {
						Console.WriteLine(QueuedLogs.Dequeue());
					}
				}
			}
		}

		static void FixWorkingDirectory ()
		{
			string[] args = Environment.GetCommandLineArgs();
			string newPath = Path.Combine(Environment.CurrentDirectory, args[0]);
			newPath = Directory.GetParent(newPath).FullName;
			Environment.CurrentDirectory = newPath;
			WriteConsole("Setting working directory to {0}", newPath);
		}

		static void ListHelp ()
		{
			WriteConsole("Available commands:");
			WriteConsole("quit                      - Exits this program");
			WriteConsole("list,help,?               - Lists available commands");
			WriteConsole("start_server              - Starts a server. Arguments possible:");
			WriteConsole("- Example: start_server +server.world \"test world\" +server.networktype LAN");
			WriteConsole("| All options and things below are case sensitive");
			WriteConsole("| +server.world           - followed by worldname to load/create");
			WriteConsole("| +server.name            - server name to display in the server browser");
			WriteConsole("| +server.networktype     - Network type to host. Possible options below:");
			WriteConsole("-| Singleplayer  - connects to a localhost client, not really usable manually");
			WriteConsole(" | LAN           - allows connecting from localhost through the ingame button");
			WriteConsole(" | SteamLAN      - steam networking, does not port forward or check authentication");
			WriteConsole(" | SteamOnline   - steam networking, port forwards and checks authentication");
			WriteConsole("| +server.maxplayers      - default 10; max players to be active at the same time");
			WriteConsole("| +server.gameport        - default 27016; port queried for info to display in the server browser");
			WriteConsole("| +server.ip              - default 0.0.0.0 (auto); IP to use when selecting local adapter");
			WriteConsole("| +server.steamport       - default 27017; port passed for 'steam use', seems unused");
			WriteConsole("| +server.usevac          - default false; whether to filter for VAC status, true or false");
			WriteConsole("| +server.seed            - if new world, seed used to generate terrain. type integer");
			WriteConsole("| +server.monsterson      - if new world, whether to spawn monsters. default true");
			WriteConsole("| +server.initialsettings - if new world, initialsettings file to use, default normal");
			WriteConsole("| +server.monstersday     - if new world, whether to spawn monsters during the day, default false");
			WriteConsole("| +server.monstersdouble  - if new world, whether to spawn double the amount of monsters, default false");
			WriteConsole("stop_server             - Stops a server");
			WriteConsole("send                    - Send text to the server");
			WriteConsole("- Example 1: send Hey Everyone!");
			WriteConsole("| Example 2: send /time add 10");
		}

		static void SendLog (string log) {
			if (!IsServerRunning) {
				WriteConsole ("No server running!");
				return;
			}
			lock (MessagesToSend) {
				MessagesToSend.Add (new KeyValuePair<string, string>("chat", log));
			}
		}

		static void StartServer (string arg) {
			if (IsServerRunning) {
				WriteConsole ("A server is already running");
				return;
			}

			string path;
			if (File.Exists("colonyserver.exe")) {
				path = "colonyserver.exe";
			} else if (File.Exists("colonyserver.x86")) {
				path = "colonyserver.x86";
			} else if (File.Exists("colonyserver.x86_64")) {
				path = "colonyserver.x86_64";
			} else if (File.Exists("colonyserver.app")) {
				path = "colonyserver.app/Contents/MacOS/colonyserver";
			} else {
				WriteConsole ("Failed to find colonyserver executable");
				return;
			}
			WriteConsole ("Found colonyserver executable: {0}", path);

			ServerListener = new TcpListener (IPAddress.Loopback, 0);
			ServerListener.Start ();
			IPEndPoint endPoint = ServerListener.LocalEndpoint as IPEndPoint;
			WriteConsole ("Started listening for logging at {0}...", endPoint);
			LoggingThread = new Thread (Logger);
			LoggingThread.Start ();
			ServerProcess = Process.Start (path, string.Format ("-batchmode -nographics +logto {0} {1}", endPoint.Port, arg));
		}

		static void StopServer () {
			if (!IsServerRunning) {
				WriteConsole ("No server was running");
				return;
			}

			WriteConsole ("Sending quit message");
			PrintQueuedLogs();
			lock (MessagesToSend) {
				MessagesToSend.Add (new KeyValuePair<string, string> ("quit", null));
			}
			while (ServerProcess != null && ServerProcess.WaitForExit (500)) {
				WriteConsole ("Waiting for server exit...");
				PrintQueuedLogs();
				Thread.Sleep(500);
			}
			WriteConsole ("Succesfully closed server");
			PrintQueuedLogs();
			ServerProcess = null;
		}

		static void Logger () {
			WriteConsole ("Starting logging thread");
			while (ServerProcess == null) {
				Thread.Sleep (0);
			}
			WriteConsole ("ServerProcess set");
			while (IsServerRunning) {
				if (ServerListener.Pending ()) {
					WriteConsole ("Server connection pending");
					using (TcpClient client = ServerListener.AcceptTcpClient ())
					using (NetworkStream clientStream = client.GetStream()) {
						BinaryReader clientReader = new BinaryReader (clientStream);
						BinaryWriter clientWriter = new BinaryWriter (clientStream);
						client.NoDelay = true;
						WriteConsole ("Server connection accepted");

						lock (MessagesToSend) {
							MessagesToSend.Clear ();
						}

						while (IsServerRunning) {
							if (MessagesToSend.Count > 0) {
								lock (MessagesToSend) {
									foreach (var send in MessagesToSend) {
										clientWriter.Write(send.Key);
										if (send.Value != null) {
											clientWriter.Write(send.Value);
										}
									}
									MessagesToSend.Clear();
								}
							}
							while (client.Available >= 1) {
								switch (clientReader.ReadString ()) {
								case "log":
									WriteConsole (clientReader.ReadString ());
									break;
								}
							}
							Thread.Sleep (50);
						}
					}
				} else {
					Thread.Sleep (50);
				}
			}
			WriteConsole ("Logging thread stopped");
		}

		static List<string> SplitLaunchArgs (string read) {
			List<string> args = new List<string> ();
			int argStart = 0;
			int argLength = 0;
			bool isEscaping = false;
			for (int i = 0; i < read.Length; i++) {
				switch (read [i]) {
				case ' ':
					if (isEscaping) {
						goto default;
					}
					if (argLength > 0) {
						args.Add (read.Substring (argStart, argLength));
					}
					argStart = i + 1;
					argLength = 0;
					break;
				case '"':
					isEscaping = !isEscaping;
					argLength++;
					break;
				default:
					argLength++;
					break;
				}
			}
			if (argLength > 0) {
				args.Add (read.Substring (argStart, argLength));
			}
			return args;
		}

		static void WriteConsole (string s, params object[] args)
		{
			if (args != null && args.Length > 0) {
				s = string.Format(s, args);
			}
			lock (QueuedLogs) {
				QueuedLogs.Enqueue(s);
			}
		}
	}
}
