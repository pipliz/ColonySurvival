using Pipliz;
using Pipliz.Chatting;
using Pipliz.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExampleMod
{
	[ModLoader.ModManager]
	public class ExampleChatModEntries
	{
		static Dictionary<NetworkID, Stopwatch> playerTimers = new Dictionary<NetworkID, Stopwatch>();
		static string uniqueSaveString = "examplechatmod.ticksplayed";

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesServer)]
		public static void AfterItemTypesServer ()
		{
			ChatCommands.CommandManager.RegisterCommand(new ExampleChatCommand());
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerConnected)]
		public static void OnPlayerConnected (Players.Player player)
		{
			// players connects; start counting on a stopwatch
			Stopwatch watch;
			if (playerTimers.TryGetValue (player.ID, out watch)) {
				watch.Start();
			} else {
				playerTimers.Add(player.ID, Stopwatch.StartNew());
			}
			// mark Player as dirty, so the OnSavingPlayer callback is triggered upon disconnect
			player.ShouldSave = true;
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerDisconnected)]
		public static void OnPlayerDisconnected (Players.Player player)
		{
			// players disconnects; stop the timer
			Stopwatch watch;
			if (playerTimers.TryGetValue(player.ID, out watch)) {
				watch.Stop();
			}
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnSavingPlayer)]
		public static void OnSavingPlayer (JSONNode node, Players.Player player)
		{
			// player data is being saved, probably because the player is disconnecting. Add elapsed time to save node
			Stopwatch watch;
			if (playerTimers.TryGetValue (player.ID, out watch)) {
				long ticks = 0;
				node.TryGetAs(uniqueSaveString, out ticks);
				ticks += watch.ElapsedTicks;
				watch.Reset();
				node.SetAs(uniqueSaveString, ticks);
			}
		}

		class ExampleChatCommand : ChatCommands.IChatCommand
		{
			public bool IsCommand (string chat)
			{
				return chat.StartsWith("/played");
			}

			public bool TryDoCommand (NetworkID owner, string chat)
			{
				Players.Player player = Players.GetPlayer(owner);
				if (player != null) {
					long ticks;
					player.SavegameNode.TryGetAs(uniqueSaveString, out ticks);
					Stopwatch watch;
					if (playerTimers.TryGetValue (owner, out watch)) {
						Chat.Send(owner, string.Format("You've played for {0:n2} seconds in the current session.", watch.Elapsed.TotalSeconds));
						ticks += watch.ElapsedTicks;
					}
					TimeSpan span = new TimeSpan(ticks);
					Chat.Send(owner, string.Format("You've played on this world for {0} days, {1} hours, {2} minutes and {3} seconds.",
						span.Days,
						span.Hours,
						span.Minutes,
						span.Seconds)
					);
				}
				return true;
			}
		}
	}
}
