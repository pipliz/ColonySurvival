﻿using Pipliz.APIProvider.Science;
using Server.Science;

namespace Pipliz.BaseResearch.Implementations
{
	[AutoLoadedResearchable]
	public class HealthSize1 : BaseResearchable
	{
		public HealthSize1 ()
		{
			key = "pipliz.baseresearch.healthsize1";
			icon = "gamedata/textures/icons/baseresearch_healthsize1.png";
			iterationCount = 25;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebaglife");
			AddDependency("pipliz.baseresearch.sciencebaglife");
		}

		public override void OnResearchComplete (ScienceManagerPlayer manager)
		{
			manager.Player.GetTempValues(true).Set("pipliz.healthmax", 125f);
			manager.Player.SendHealthPacket();
		}
	}
}
