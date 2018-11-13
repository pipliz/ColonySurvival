using BlockTypes;
using Jobs;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class Hollyhock : AbstractFarmAreaJobDefinition<Hollyhock>
	{
		public Hollyhock ()
		{
			Identifier = "pipliz.hollyhockfarm";
			fileName = "hollyhockfarms";
			Stages = new ushort[] {
				BuiltinBlocks.HollyhockStage1,
				BuiltinBlocks.HollyhockStage2
			};
			UsedNPCType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.hollyhockfarmer");
			AreaType = Shared.EAreaType.HollyhockFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			if (!isLoaded) {
				TurnArableIntoDirt(min, max, owner);
			}
			return new FarmAreaJob<Hollyhock>(owner, min, max, npcID);
		}
	}
}