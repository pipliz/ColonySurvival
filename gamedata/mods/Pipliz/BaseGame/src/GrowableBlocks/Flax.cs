using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	public class Flax : BaseGrowableBlockDefinition
	{
		public Flax ()
		{
			GrowthType = EGrowthType.FirstNightRandom;
			SetStages(new List<GrowableStage>()
			{
				new GrowableStage("flaxstage1"),
				new GrowableStage("flaxstage2")
			});
		}
	}
}
