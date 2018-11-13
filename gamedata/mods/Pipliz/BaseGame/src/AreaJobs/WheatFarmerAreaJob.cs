using BlockTypes;
using Jobs;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class WheatFarmerDefinition : AbstractFarmAreaJobDefinition<WheatFarmerDefinition>
	{
		public WheatFarmerDefinition ()
		{
			Identifier = "pipliz.wheatfarm";
			fileName = "wheatfarms";
			Stages = new ushort[] {
				BuiltinBlocks.WheatStage1,
				BuiltinBlocks.WheatStage2,
				BuiltinBlocks.WheatStage3
			};
			UsedNPCType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.wheatfarmer");
			AreaType = Shared.EAreaType.WheatFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			if (!isLoaded) {
				TurnArableIntoDirt(min, max, owner);
			}
			return new FarmAreaJob<WheatFarmerDefinition>(owner, min, max, npcID);
		}
	}
}
