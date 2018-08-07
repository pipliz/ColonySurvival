using NPC;
using static NPC.NPCBase;

namespace Pipliz.APIProvider.Jobs
{
	public interface IBlockJobSettings
	{
		ItemTypes.ItemType[] BlockTypes { get; }
		NPCType NPCType { get; }
		InventoryItem RecruitmentItem { get; }
		bool ToSleep { get; }
		Vector3Int GetJobLocation (BlockJobInstance instance);
		void OnNPCAtJob (BlockJobInstance instance, ref NPCState state);
		void OnNPCAtStockpile (BlockJobInstance instance, ref NPCState state);
		void OnGoalChanged (BlockJobInstance instance, NPCGoal goalOld, NPCGoal goalNew);
	}
}
