using BlockTypes;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using Areas;

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
			npcType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.wolfsbanefarm");
			areaType = Shared.EAreaType.WolfsbaneFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			// todo use colony as param
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner.Owners[0]);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}