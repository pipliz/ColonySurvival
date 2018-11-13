using BlockEntities;
using GrowableBlocks;
using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	[BlockEntityAutoLoader]
	public class Wolfsbane : BaseGrowableBlockDefinition
	{
		public Wolfsbane ()
		{
			GrowthType = EGrowthType.FirstNightRandom;
			SetStages(new List<GrowableStage>()
			{
				new GrowableStage("wolfsbanestage1"),
				new GrowableStage("wolfsbanestage2")
			});
		}
	}
}
