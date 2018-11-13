using BlockTypes;
using Jobs;
using NPC;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	[AreaJobDefinitionAutoLoader]
	public class FlaxFarmerDefinition : AbstractFarmAreaJobDefinition<FlaxFarmerDefinition>
	{
		public FlaxFarmerDefinition ()
		{
			Identifier = "pipliz.flaxfarm";
			fileName = "flaxfarms";
			Stages = new ushort[] {
				BuiltinBlocks.FlaxStage1,
				BuiltinBlocks.FlaxStage2
			};
			UsedNPCType = NPCType.GetByKeyNameOrDefault("pipliz.flaxfarmer");
			AreaType = Shared.EAreaType.FlaxFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			if (!isLoaded) {
				TurnArableIntoDirt(min, max, owner);
			}
			return new FarmAreaJob<FlaxFarmerDefinition>(owner, min, max, npcID);
		}
	}
}