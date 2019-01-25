using Jobs;
using NPC;

namespace Pipliz.Mods.BaseGame.Construction
{
	using JSON;

	[AreaJobDefinitionAutoLoader]
	public class ConstructionAreaDefinition : AbstractAreaJobDefinition
	{
		public ConstructionAreaDefinition () : base()
		{
			base.UsedNPCType = NPCType.GetByKeyNameOrDefault("pipliz.constructor");
			base.Identifier = "pipliz.constructionarea";
			MaxGathersPerRun = 5;
		}

		// need to parse some additional data that the default one doesn't
		public override IAreaJob CreateAreaJob (Colony owner, JSONNode node)
		{
			Vector3Int min = new Vector3Int()
			{
				x = node.GetAsOrDefault("x-", int.MinValue),
				y = node.GetAsOrDefault("y-", int.MinValue),
				z = node.GetAsOrDefault("z-", int.MinValue)
			};

			Vector3Int max = min + new Vector3Int()
			{
				x = node.GetAsOrDefault("xd", 0),
				y = node.GetAsOrDefault("yd", 0),
				z = node.GetAsOrDefault("zd", 0)
			};

			JSONNode args;
			if (!node.TryGetChild("args", out args)) {
				return null;
			}

			ConstructionArea area = (ConstructionArea)CreateAreaJob(owner, min, max, true);
			area.SetArgument(args);

			return area;
		}

		public override IAreaJob CreateAreaJob (Colony owner, Vector3Int min, Vector3Int max, bool isLoaded, int npcID = 0)
		{
			// there's no npc ID to load - as npcs dont work directly on these areas
			return new ConstructionArea(this, owner, min, max);
		}
	}
}