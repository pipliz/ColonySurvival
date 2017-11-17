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
		static List<char> InputChars = new List<char> ();

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

		public static void Main (string[] args)
		{
			Console.Title = "Colony Survival Dedicated Server";
			WriteConsole("Launching Colony Survival Dedicated Server");

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
				string read = null;
				while (read == null) {
					if (Console.KeyAvailable) {
						ReadConsoleKey(ref read);
					} else {
						Thread.Sleep(1);
					}
				}
				read = read.TrimStart(' ', '\t', '\n', '\r');
				if (string.IsNullOrEmpty(read)) {
					ListHelp();
					continue;
				}
				string start = read;
				if (read.Contains(" ")) {
					start = read.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
				}
				switch (start) {
					case "quit":
						WriteConsole(string.Format("> {0}", read));
						if (IsServerRunning) {
							StopServer();
						}
						return;
					case "help":
					case "list":
					case "?":
						WriteConsole(string.Format("> {0}", read));
						ListHelp();
						break;
					case "start_server":
						if (read.Length < "start_server  ".Length) {
							goto case "help";
						}
						WriteConsole(string.Format("> {0}", read));
						StringBuilder str = new StringBuilder();
						str.Append(read, "start_server ".Length, read.Length - "start_server ".Length);
						str.AppendFormat(" +parentprocess {0} ", Process.GetCurrentProcess().Id);
						StartServer(str.ToString());
						break;
					case "stop_server":
						WriteConsole(string.Format("> {0}", read));
						StopServer();
						break;
					case "send":
						if (read.Length < "send  ".Length) {
							goto case "help";
						}
						WriteConsole(string.Format("> {0}", read));
						SendLog(read.Remove(0, "send ".Length));
						break;
					default:
						WriteConsole("Unexpected command: {0}", read);
						break;
				}
			}
		}

		static void ReadConsoleKey (ref string read) {
			ConsoleKeyInfo key = Console.ReadKey (true);

			switch (key.Key) {
			case ConsoleKey.Backspace:
				if (InputChars.Count > 0) {
					InputChars.RemoveAt (InputChars.Count - 1);
				}
				break;
			case ConsoleKey.Enter:
				read = new string (InputChars.ToArray ());
				InputChars.Clear ();
				break;
			case ConsoleKey.Escape:
				InputChars.Clear ();
				break;
			case ConsoleKey.Home:
			case ConsoleKey.Tab:
				break;
			default:
				if (key.KeyChar == '\u0000') {
					break;
				}
				InputChars.Add (key.KeyChar);
				break;
			}
			Console.CursorLeft = 0;
			Console.Write (new string (' ', Console.BufferWidth - 1));
			PrintCurrentTyping ();
		}

		static void PrintCurrentTyping () {
			Console.CursorLeft = 0;
			string toPrint;
			if (InputChars.Count < Console.BufferWidth) {
				toPrint = new string (InputChars.ToArray ());
			} else {
				char[] chars = new char[Console.BufferWidth - 1];
				int tooMany = InputChars.Count - Console.BufferWidth + 1;

				for (int i = 0; i < chars.Length; i++) {
					chars [i] = InputChars [i + tooMany];
				}
				toPrint = new string (chars);
			}
			Console.Write (toPrint);
		}

		static void ListHelp () {
			WriteConsole ("Available commands:");
			WriteConsole ("quit                      - Exits this program");
			WriteConsole ("list,help,?               - Lists available commands");
			WriteConsole ("start_server              - Starts a server. Arguments possible:");
			WriteConsole ("| +server.world           - followed by worldname to load/create");
			WriteConsole ("| +server.name            - server name to display in the server browser");
			WriteConsole ("| +server.networktype     - Network type to host as, options are Singleplayer (connects to client), LAN (localhost client connects), SteamLAN, SteamOnline");
			WriteConsole ("| +server.maxplayers      - max players to be active at the same time, default 10");
			WriteConsole ("| +server.gameport        - port used by the game (for discovery mostly) default 27016");
			WriteConsole ("| +server.ip              - which IP to use to select the network adapter. Not needed in most cases. 0.0.0.0 for auto");
			WriteConsole ("| +server.steamport       - port used by steam, default 27017");
			WriteConsole ("| +server.usevac          - whether to filter for VAC status, true or false. Untested, default false.");
			WriteConsole ("| +server.seed            - if new world, seed used to generate terrain. type integer");
			WriteConsole ("| +server.monsterson      - if new world, whether to spawn monsters. default true");
			WriteConsole ("| +server.initialsettings - if new world, initialsettings file to use, default normal");
			WriteConsole ("| +server.monstersday     - if new world, whether to spawn monsters during the day, default false");
			WriteConsole ("| +server.monstersdouble  - if new world, whether to spawn double the amount of monsters, default false");
			WriteConsole ("stop_server             - Stops a server");
			WriteConsole ("send                    - Send text to the server (example: send Hey Everyone!)");
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
			if (File.Exists ("colonyserver.exe")) {
				path = "colonyserver.exe";
			} else if (File.Exists ("colonyserver.x86")) {
				path = "colonyserver.x86";
			} else if (File.Exists ("colonyserver.x86_64")) {
				path = "colonyserver.x86_64";
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
			ServerProcess = System.Diagnostics.Process.Start (path, string.Format ("-batchmode -nographics +logto {0} {1}", endPoint.Port, arg));
		}

		static void StopServer () {
			if (!IsServerRunning) {
				WriteConsole ("No server was running");
				return;
			}

			WriteConsole ("Sending quit message");
			lock (MessagesToSend) {
				MessagesToSend.Add (new KeyValuePair<string, string> ("quit", null));
			}
			while (ServerProcess != null && ServerProcess.WaitForExit (2500)) {
				WriteConsole ("Waiting for server exit...");
			}
			WriteConsole ("Succesfully closed server");
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
							while (client.Available >= 1) {
								switch (clientReader.ReadString ()) {
								case "log":
									WriteConsole (clientReader.ReadString ());
									break;
								}
							}
							if (MessagesToSend.Count > 0) {
								lock (MessagesToSend) {
									foreach (var send in MessagesToSend) {
										clientWriter.Write (send.Key);
										if (send.Value != null) {
											clientWriter.Write (send.Value);
										}
									}
									MessagesToSend.Clear ();
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

		static void WriteConsole (string s, params object[] args) {
			Console.CursorLeft = 0;
			Console.Write (new String (' ', Console.BufferWidth - 1));
			Console.CursorLeft = 0;
			if (args != null && args.Length > 0) {
				Console.WriteLine(s, args);
			} else {
				Console.WriteLine(s);
			}
			PrintCurrentTyping ();
		}
	}
}
