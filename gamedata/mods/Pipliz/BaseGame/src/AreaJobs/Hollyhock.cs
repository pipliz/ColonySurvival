using BlockTypes;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using Areas;

	[AreaJobDefinitionAutoLoader]
	public class Hollyhock : AreaJobDefinitionDefault<Hollyhock>
	{
		public Hollyhock ()
		{
			identifier = "pipliz.hollyhockfarm";
			fileName = "hollyhockfarms";
			stages = new ushort[] {
				BuiltinBlocks.HollyhockStage1,
				BuiltinBlocks.HollyhockStage2
			};
			npcType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.hollyhockfarmer");
			areaType = Shared.EAreaType.HollyhockFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			// todo use colony as param
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner.Owners[0]);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}