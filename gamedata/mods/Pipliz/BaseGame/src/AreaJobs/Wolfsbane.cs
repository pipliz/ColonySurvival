using BlockTypes;
using Jobs;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class Wolfsbane : AbstractFarmAreaJobDefinition<Wolfsbane>
	{
		public Wolfsbane ()
		{
			Identifier = "pipliz.wolfsbanefarm";
			fileName = "wolfsbanefarms";
			Stages = new ushort[] {
				BuiltinBlocks.WolfsbaneStage1,
				BuiltinBlocks.WolfsbaneStage2
			};
			UsedNPCType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.wolfsbanefarm");
			AreaType = Shared.EAreaType.WolfsbaneFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			if (!isLoaded) {
				TurnArableIntoDirt(min, max, owner);
			}
			return new FarmAreaJob<Wolfsbane>(owner, min, max, npcID);
		}
	}
}