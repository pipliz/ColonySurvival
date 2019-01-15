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
		[ModLoader.ModCallbackDependsOn("create_servermanager_trackers")]
		[ModLoader.ModCallbackProvidesFor("pipliz.blocknpcs.registerjobs")]
		static void AfterDefiningNPCTypes ()
		{
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<MinerJobInstance>(new MinerJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<ScientistJobInstance>(new ScientistJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<ConstructionJobInstance>(new ConstructionJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<WaterGathererInstance>(new WaterGathererSettings()));
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnActiveColonyChanges, "sendconstructiondata")]
		static void SendData (Players.Player player, Colony oldColony, Colony newColony)
		{
			ConstructionHelper.OnColonyChange(player, oldColony, newColony);
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnModifyResearchables, "farmingresults")]
		static void AddFarmingResults (Dictionary<string, BaseResearchable> dict)
		{
			RegisterCallback(dict, "pipliz.baseresearch.flaxfarming", (BaseResearchable res, ColonyScienceState manager, EResearchCompletionReason reason) =>
			{
				if (reason == EResearchCompletionReason.ProgressCompleted) {
					manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.FlaxStage1, 100);
					SendMessage(manager.Colony, "You received 100 flax seeds!");
				}
			});

			RegisterCallback(dict, "pipliz.baseresearch.herbfaming", (BaseResearchable res, ColonyScienceState manager, EResearchCompletionReason reason) =>
			{
				if (reason == EResearchCompletionReason.ProgressCompleted) {
					manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.AlkanetStage1, 100);
					manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.HollyhockStage1, 100);
					manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.WolfsbaneStage1, 100);
					SendMessage(manager.Colony, "You received 100 Alkanet, Hollyhock and Wolfsbane seeds!");
				}
			});

			RegisterCallback(dict, "pipliz.baseresearch.wheatfarming", (BaseResearchable res, ColonyScienceState manager, EResearchCompletionReason reason) =>
			{
				if (reason == EResearchCompletionReason.ProgressCompleted) {
					manager.Colony.Stockpile.Add(BlockTypes.BuiltinBlocks.WheatStage1, 400);
					SendMessage(manager.Colony, "You received 400 wheat seeds!");
				}
			});

			foreach (var pair in BuilderLimits) {
				RegisterCallback(dict, pair.Key, BuilderLimitResearch);
			}

			foreach (var pair in DiggerLimits) {
				RegisterCallback(dict, pair.Key, DiggerLimitResearch);
			}
		}

		static void BuilderLimitResearch (BaseResearchable res, ColonyScienceState manager, EResearchCompletionReason reason)
		{
			int current = manager.Colony.TemporaryData.GetAsOrDefault("pipliz.builderlimit", 0);
			int next = BuilderLimits[res.GetKey()];
			manager.Colony.TemporaryData.SetAs("pipliz.builderlimit", Math.Max(current, next));
			ConstructionHelper.SendPacket(manager.Colony);
		}

		static void DiggerLimitResearch (BaseResearchable res, ColonyScienceState manager, EResearchCompletionReason reason)
		{
			int current = manager.Colony.TemporaryData.GetAsOrDefault("pipliz.diggerlimit", 0);
			int next = DiggerLimits[res.GetKey()];
			manager.Colony.TemporaryData.SetAs("pipliz.diggerlimit", Math.Max(current, next));
			ConstructionHelper.SendPacket(manager.Colony);
		}

		static void RegisterCallback (Dictionary<string, BaseResearchable> dict, string key, BaseResearchable.OnCompleteAction action)
		{
			if (dict.TryGetValue(key, out BaseResearchable res)) {
				res.AddOnCompleteAction(action);
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
