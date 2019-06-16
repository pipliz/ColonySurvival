using Jobs;
using Pipliz.Mods.BaseGame.Construction;
using Science;
using Shared.Networking;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		public static Dictionary<string, int> BuilderLimits = new Dictionary<string, int>()
		{
			{"pipliz.baseresearch.constructionbuilder", 1000 },
			{"pipliz.baseresearch.constructionbuildersize1", 25000 },
			{"pipliz.baseresearch.constructionbuildersize2", 100000 },
			{"pipliz.baseresearch.constructionbuildersize3", 250000 },
			{"pipliz.baseresearch.constructionbuildersize4", 500000 },
			{"pipliz.baseresearch.constructionbuildersize5", 1000000 }
		};

		public static Dictionary<string, int> DiggerLimits = new Dictionary<string, int>()
		{
			{"pipliz.baseresearch.constructiondigger", 5000 },
			{"pipliz.baseresearch.constructiondiggersize1", 25000 },
			{"pipliz.baseresearch.constructiondiggersize2", 100000 },
			{"pipliz.baseresearch.constructiondiggersize3", 250000 },
			{"pipliz.baseresearch.constructiondiggersize4", 500000 },
			{"pipliz.baseresearch.constructiondiggersize5", 1000000 }
		};

		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "register.basegame.blockjobs")]
		[ModLoader.ModCallbackDependsOn("create_servermanager_trackers")] // creates BlockEntityCallbacks there
		[ModLoader.ModCallbackDependsOn("pipliz.server.loadnpctypes")] // resolve npc types per block job types
		[ModLoader.ModCallbackProvidesFor("create_savemanager")] // want the jobs to exist before any jobs' blocks are loaded
		static void AfterDefiningNPCTypes ()
		{
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<MinerJobInstance>(new MinerJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<ScientistJobInstance>(new ScientistJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<ConstructionJobInstance>(new ConstructionJobSettings()));
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnActiveColonyChanges, "sendconstructiondata")]
		static void SendData (Players.Player player, Colony oldColony, Colony newColony)
		{
			ConstructionHelper.OnColonyChange(player, oldColony, newColony);
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnModifyResearchables, "farmingresults")]
		static void AddFarmingResults (Dictionary<string, DefaultResearchable> dict)
		{
			foreach (var pair in BuilderLimits) {
				RegisterCallback(dict, pair.Key, BuilderLimitResearch);
			}

			foreach (var pair in DiggerLimits) {
				RegisterCallback(dict, pair.Key, DiggerLimitResearch);
			}
		}

		static void BuilderLimitResearch (AbstractResearchable res, ColonyScienceState manager, EResearchCompletionReason reason)
		{
			int current = manager.Colony.TemporaryData.GetAsOrDefault("pipliz.builderlimit", 0);
			int next = BuilderLimits[res.GetKey()];
			manager.Colony.TemporaryData.SetAs("pipliz.builderlimit", Math.Max(current, next));
			ConstructionHelper.SendPacket(manager.Colony);
		}

		static void DiggerLimitResearch (AbstractResearchable res, ColonyScienceState manager, EResearchCompletionReason reason)
		{
			int current = manager.Colony.TemporaryData.GetAsOrDefault("pipliz.diggerlimit", 0);
			int next = DiggerLimits[res.GetKey()];
			manager.Colony.TemporaryData.SetAs("pipliz.diggerlimit", Math.Max(current, next));
			ConstructionHelper.SendPacket(manager.Colony);
		}

		static void RegisterCallback (Dictionary<string, DefaultResearchable> dict, string key, DefaultResearchable.OnCompleteAction action)
		{
			if (dict.TryGetValue(key, out DefaultResearchable res)) {
				res.AddOnCompleteAction(action);
			} else {
				Log.WriteWarning($"Tried to register callback to research {key}, which was not registered");
			}
		}

		static void SendMessage (Colony colony, string message)
		{
			foreach (var owner in colony.Owners) {
				if (owner.ShouldSendData) {
					Chatting.Chat.Send(owner, message);
				}
			}
		}

		public static class ConstructionHelper
		{
			public static void OnColonyChange (Players.Player player, Colony oldColony, Colony newColony)
			{
				if (player.ShouldSendData && newColony != null) {
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
}
