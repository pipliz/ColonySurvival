using Jobs;
using Pipliz.Mods.BaseGame.Construction;

namespace Pipliz.Mods.BaseGame
{
	[ModLoader.ModManager]
	public static class ModEntries
	{
		[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "register.basegame.blockjobs")]
		[ModLoader.ModCallbackDependsOn("create_servermanager_trackers")]
		[ModLoader.ModCallbackProvidesFor("pipliz.blocknpcs.registerjobs")]
		static void AfterDefiningNPCTypes ()
		{
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<MinerJobInstance>(new MinerJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<BlockJobInstance>(new ScientistJobSettings()));
			ServerManager.BlockEntityCallbacks.RegisterEntityManager(new BlockJobManager<ConstructionJobInstance>(new ConstructionJobSettings()));
		}

		[ModLoader.ModCallback(ModLoader.EModCallbackType.OnActiveColonyChanges, "sendconstructiondata")]
		static void SendData (Players.Player player, Colony oldColony, Colony newColony)
		{
			Researches.ConstructionHelper.OnColonyChange(player, oldColony, newColony);
		}
	}
}
