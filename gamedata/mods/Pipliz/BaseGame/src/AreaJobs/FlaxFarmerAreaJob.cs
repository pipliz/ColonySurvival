using BlockTypes;

namespace Pipliz.Mods.BaseGame.AreaJobs
{
	using APIProvider.AreaJobs;
	using Areas;
	using NPC;

	[AreaJobDefinitionAutoLoader]
	public class FlaxFarmerDefinition : AreaJobDefinitionDefault<FlaxFarmerDefinition>
	{
		public FlaxFarmerDefinition ()
		{
			identifier = "pipliz.flaxfarm";
			fileName = "flaxfarms";
			stages = new ushort[] {
				BuiltinBlocks.FlaxStage1,
				BuiltinBlocks.FlaxStage2
			};
			npcType = NPCType.GetByKeyNameOrDefault("pipliz.flaxfarmer");
			areaType = Shared.EAreaType.FlaxFarm;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, int npcID = 0)
		{
			// todo: use colony as param
			SetLayer(min, max, BuiltinBlocks.Dirt, -1, owner.Owners[0]);
			return base.CreateAreaJob(owner, min, max, npcID);
		}
	}
}