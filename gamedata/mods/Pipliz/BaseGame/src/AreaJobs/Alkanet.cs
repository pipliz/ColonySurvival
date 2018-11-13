using BlockTypes;
using Jobs;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class Alkanet : AbstractFarmAreaJobDefinition<Alkanet>
	{
		public Alkanet ()
		{
			Identifier = "pipliz.alkanetfarm";
			UsedNPCType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.alkanetfarmer");
			AreaType = Shared.EAreaType.AlkanetFarm;
			Stages = new ushort[] {
				BuiltinBlocks.AlkanetStage1,
				BuiltinBlocks.AlkanetStage2
			};
			fileName = "alkanetfarms";
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			if (!isLoaded) {
				TurnArableIntoDirt(min, max, owner);
			}
			return new FarmAreaJob<Alkanet>(owner, min, max, npcID);
		}
	}
}