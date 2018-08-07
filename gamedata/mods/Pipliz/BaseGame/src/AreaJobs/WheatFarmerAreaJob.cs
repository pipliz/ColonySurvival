using BlockTypes;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using Areas;

	[AreaJobDefinitionAutoLoader]
	public class WheatFarmerDefinition : AreaJobDefinitionDefault<WheatFarmerDefinition>
	{
		public WheatFarmerDefinition ()
		{
			identifier = "pipliz.wheatfarm";
			fileName = "wheatfarms";
			stages = new ushort[] {
				BuiltinBlocks.WheatStage1,
				BuiltinBlocks.WheatStage2,
				BuiltinBlocks.WheatStage3
			};
			npcType = NPC.NPCType.GetByKeyNameOrDefault("pipliz.wheatfarmer");
			areaType = Shared.EAreaType.WheatFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			// todo use colony as param
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner.Owners[0]);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}
