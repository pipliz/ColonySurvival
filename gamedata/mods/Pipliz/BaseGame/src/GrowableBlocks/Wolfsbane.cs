using Server.GrowableBlocks;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	[GrowableBlockDefinitionAutoLoader]
	public class Wolfsbane : GrowableBlockDefinition<Wolfsbane>
	{
		public Wolfsbane ()
		{
			FileName = "wolfsbane";
			GrowthType = EGrowthType.FirstNightRandom;
			Stages = new List<IGrowableStage>()
			{
				new GrowableStage("wolfsbanestage1", TimeCycle.NightLength - 0.5f),
				new GrowableStage("wolfsbanestage2")
			};
		}
	}
}
