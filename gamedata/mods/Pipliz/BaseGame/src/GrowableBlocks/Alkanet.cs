using Server.GrowableBlocks;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	[GrowableBlockDefinitionAutoLoader]
	public class Alkanet : GrowableBlockDefinition<Alkanet>
	{
		public Alkanet ()
		{
			FileName = "alkanet";
			GrowthType = EGrowthType.FirstNightRandom;
			Stages = new List<IGrowableStage>()
			{
				new GrowableStage("alkanetstage1", TimeCycle.NightLength - 0.5f),
				new GrowableStage("alkanetstage2")
			};
		}
	}
}
