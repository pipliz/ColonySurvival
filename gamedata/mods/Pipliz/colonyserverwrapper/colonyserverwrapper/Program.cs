using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Diagnostics;

namespace ColonyServerWrapper
{
	static class MainClass
	{
		static Process ServerProcess = null;

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
			Console.WriteLine ("Launching Colony Survival Dedicated Server");
			while (true) {
				string read = Console.ReadLine ();
				if (read == null) {
					if (IsServerRunning) {
						StopServer ();
					}
					return;
				}
				read = read.TrimStart (' ', '\t', '\n', '\r');
				if (string.IsNullOrEmpty (read)) {
					ListHelp ();
					continue;
				}
				string start = read;
				if (read.Contains (" ")) {
					start = read.Split (new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
				}
				switch (start) {
				case "quit":
					return;
				case "help":
				case "list":
				case "?":
					ListHelp ();
					break;
				case "start_server_new":
					if (WriteLaunchFileNew (read)) {
						StartServer ();
					} else {
						Console.WriteLine ();
						goto case "help";
					}
					break;
				case "start_server_load":
					if (WriteLaunchFileLoad (read)) {
						StartServer ();
					} else {
						Console.WriteLine ();
						goto case "help";
					}
					break;
				case "stop_server":
					StopServer ();
					break;
				default:
					Console.WriteLine("Unexpected command: {0}", read);
					break;
				}
			}
		}

		static void ListHelp () {
			Console.WriteLine ("Available commands:");
			Console.WriteLine ("quit                    - Exits this program");
			Console.WriteLine ("list,help,?             - Lists available commands");
			Console.WriteLine ("start_server_new        - Starts a server with a new world.Arguments required:");
			Console.WriteLine ("| Full example: start_server_new \"fancy colony\" \"SteamLAN\" true false false 4343434");
			Console.WriteLine ("| Arguments required below (in order, only values)");
			Console.WriteLine ("| worldname             - example: \"fancy colony\"");
			Console.WriteLine ("| network type          - possibles: [\"SteamOnline\", \"LAN\", \"SteamLAN\"]");
			Console.WriteLine ("| monsters on?          - possibles: [true, false]");
			Console.WriteLine ("| daytime monsters?     - possibles: [true, false]");
			Console.WriteLine ("| double monsters?      - possibles: [true, false]");
			Console.WriteLine ("| seed                  - example: 350295302");
			Console.WriteLine ("start_server_load       - Starts a server to load a world");
			Console.WriteLine ("| Full example: start_server_load \"fancy colony\" \"SteamLAN\"");
			Console.WriteLine ("| Arguments required below (in order, only values)");
			Console.WriteLine ("| worldname             - example: \"fancy colony\"");
			Console.WriteLine ("| network type          - possibles: [\"SteamOnline\", \"LAN\", \"SteamLAN\"]");
			Console.WriteLine ("stop_server             - Stops a server");
		}

		static void StartServer () {
			if (IsServerRunning) {
				Console.WriteLine ("A server is already running");
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
				Console.WriteLine ("Failed to find colonyserver executable");
				return;
			}
			Console.WriteLine ("Found colonyserver executable: {0}", path);
			ServerProcess = System.Diagnostics.Process.Start (path, "-batchmode -nographics -uselaunchfile");
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

		static bool WriteLaunchFileNew (string read) {
			Directory.CreateDirectory ("gamedata/savegames");
			List<string> args = SplitLaunchArgs (read);
			if (args.Count != 7) {
				Console.WriteLine ("Invalid argument count");
				return false;
			}

			if (!EnsureInQuotes (args [1], args [2])) {
				return false;
			}

			if (!EnsureIsBool (args [3], args [4], args [5])) {
				return false;
			}

			int i = 0;
			if (!int.TryParse (args[6], out i)) {
				Console.WriteLine ("Could not read {0} as number", args[6]);
				return false;
			}

			Dictionary<string, string> keyValues = new Dictionary<string, string> ();
			keyValues ["\"worldname\""] = args [1];
			keyValues ["\"networkType\""] = args [2];
			keyValues ["\"isNewWorld\""] = "true";
			keyValues ["\"monsterson\""] = args [3];
			keyValues ["\"monstersday\""] = args [4];
			keyValues ["\"monstersdouble\""] = args [5];
			keyValues ["\"seed\""] = args [6];

			WriteLaunchFile (keyValues);
			return true;
		}

		static bool WriteLaunchFileLoad (string read) {
			Directory.CreateDirectory ("gamedata/savegames");
			List<string> args = SplitLaunchArgs (read);
			if (args.Count != 3) {
				Console.WriteLine ("Invalid argument count");
				return false;
			}

			if (!EnsureInQuotes (args [1], args [2])) {
				return false;
			}

			Dictionary<string, string> keyValues = new Dictionary<string, string> ();
			keyValues ["\"worldname\""] = args [1];
			keyValues ["\"networkType\""] = args [2];
			keyValues ["\"isNewWorld\""] = "false";

			WriteLaunchFile (keyValues);
			return true;
		}

		static bool EnsureInQuotes (params string[] s) {
			for (int i = 0; i < s.Length; i++) {
				if (!s [i].StartsWith ("\"") || !s [i].EndsWith ("\"")) {
					Console.WriteLine ("Argument {0} must be in quotes", s [i]);
					return false;
				}
			}
			return true;
		}

		static bool EnsureIsBool (params string[] s) {
			for (int i = 0; i < s.Length; i++) {
				if (s[i] != "true" && s[i] != "false") {
					Console.WriteLine ("Argument {0} must be either true or false", s [i]);
					return false;
				}
			}
			return true;
		}

		static void WriteLaunchFile (Dictionary<string, string> pairs) {
			const string path = "gamedata/savegames/serverStartArgs.json";

			using (FileStream fs = File.Open (path, FileMode.Create, FileAccess.Write, FileShare.Write)) 
			using (StreamWriter writer = new StreamWriter(fs, System.Text.Encoding.UTF8, 512)) {
				writer.WriteLine ('{');
				int count = 0;
				foreach (var pair in pairs) {
					writer.WriteLine ("{0}:{1}{2}", pair.Key, pair.Value, ++count < pairs.Count ? "," : "");
				}
				writer.WriteLine ('}');
			}
		}

		static void StopServer () {
			if (!IsServerRunning) {
				Console.WriteLine ("No server was running");
				return;
			}

			bool result = ServerProcess.CloseMainWindow ();
			Console.WriteLine ("Trying to close window: {0}", result);
			if (result) {
				while (true) {
					if (ServerProcess.WaitForExit (2500)) {
						Console.WriteLine ("Succesfully closed server");
						ServerProcess.Close ();
						ServerProcess = null;
						return;
					} else {
						Console.WriteLine ("Waiting for server exit...");
					}
				}
			}
			ServerProcess = null;
		}
	}
}
