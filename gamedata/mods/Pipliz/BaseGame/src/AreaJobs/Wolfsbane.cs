using BlockTypes.Builtin;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;

	[AreaJobDefinitionAutoLoader]
	public class Wolfsbane : AreaJobDefinitionDefault<Wolfsbane>
	{
		public Wolfsbane ()
		{
			identifier = "pipliz.wolfsbanefarm";
			fileName = "wolfsbanefarms";
			stages = new ushort[] {
				BuiltinBlocks.WolfsbaneStage1,
				BuiltinBlocks.WolfsbaneStage2
			};
			npcType = Server.NPCs.NPCType.GetByKeyNameOrDefault("pipliz.wolfsbanefarm");
		}

		public override IAreaJob CreateAreaJob (Players.Player owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}