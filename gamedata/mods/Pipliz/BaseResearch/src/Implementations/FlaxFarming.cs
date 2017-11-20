using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class FlaxFarming : BaseResearchable
	{
		public FlaxFarming ()
		{
			key = "pipliz.baseresearch.flaxfarming";
			icon = "gamedata/textures/icons/flax.png";
			iterationCount = 3;
			AddIterationRequirement("berry", 3);
			AddIterationRequirement("coppernails");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager, EResearchCompletionReason reason)
		{
			if (reason == EResearchCompletionReason.ProgressCompleted) {
				Stockpile.GetStockPile(manager.Player).Add(BlockTypes.Builtin.BuiltinBlocks.FlaxStage1, 100);
				if (manager.Player.IsConnected) {
					Chatting.Chat.Send(manager.Player, "You received 100 flax seeds!");
				}
			}
		}
	}
}
