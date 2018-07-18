using System.Collections.Generic;

namespace Pipliz.Mods.BaseGame.GrowableBlocks
{
	using APIProvider.GrowableBlocks;

	public class Hollyhock : BaseGrowableBlockDefinition
	{
		public Hollyhock ()
		{
			GrowthType = EGrowthType.FirstNightRandom;
			SetStages(new List<GrowableStage>()
			{
				new GrowableStage("hollyhockstage1"),
				new GrowableStage("hollyhockstage2")
			});
		}
	}
}
