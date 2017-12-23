using Server.GrowableBlocks;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	[GrowableBlockDefinitionAutoLoader]
	public class Hollyhock : GrowableBlockDefinition<Hollyhock>
	{
		public Hollyhock ()
		{
			FileName = "hollyhock";
			GrowthType = EGrowthType.FirstNightRandom;
			Stages = new List<IGrowableStage>()
			{
				new GrowableStage("hollyhockstage1", TimeCycle.NightLength - 0.5f),
				new GrowableStage("hollyhockstage2")
			};
		}
	}
}
