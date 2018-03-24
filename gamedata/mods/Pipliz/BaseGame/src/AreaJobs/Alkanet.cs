using BlockTypes.Builtin;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;

	[AreaJobDefinitionAutoLoader]
	public class Alkanet : AreaJobDefinitionDefault<Alkanet>
	{
		public Alkanet ()
		{
			identifier = "pipliz.alkanetfarm";
			fileName = "alkanetfarms";
			stages = new ushort[] {
				BuiltinBlocks.AlkanetStage1,
				BuiltinBlocks.AlkanetStage2
			};
			npcType = Server.NPCs.NPCType.GetByKeyNameOrDefault("pipliz.alkanetfarmer");
			areaType = Shared.EAreaType.AlkanetFarm;
		}

		public override IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}