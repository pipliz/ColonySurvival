using BlockTypes;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using Areas;

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
			npcType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.alkanetfarmer");
			areaType = Shared.EAreaType.AlkanetFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			// todo: use colony as param of setlayer
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner.Owners[0]);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}