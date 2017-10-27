﻿using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthRegenSpeed1 : BaseResearchable
	{
		public HealthRegenSpeed1 ()
		{
			key = "pipliz.baseresearch.healthregenspeed1";
			icon = "gamedata/textures/icons/baseresearch_healthregenspeed1.png";
			iterationCount = 15;
			AddIterationRequirement("sciencebagbasic", 2);
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.sciencebaglife");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthregenspeed", 2f);
		}
	}
}
