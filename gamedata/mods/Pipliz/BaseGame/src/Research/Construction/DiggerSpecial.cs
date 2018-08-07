
using Science;

namespace Pipliz.Mods.BaseGame.Researches
{
	[AutoLoadedResearchable]
	public class ConstructionDiggerSpecial : BaseResearchable
	{
		public ConstructionDiggerSpecial ()
		{
			key = "pipliz.baseresearch.constructiondiggerspecial";
			icon = "gamedata/textures/icons/bronzeaxe.png";
			iterationCount = 50;
			AddIterationRequirement("sciencebagbasic");
			AddIterationRequirement("sciencebagadvanced");
			AddDependency("pipliz.baseresearch.constructionbuilder");
			AddDependency("pipliz.baseresearch.sciencebagadvanced");
		}

		public override void OnResearchComplete (ColonyScienceState manager, EResearchCompletionReason reason)
		{
		}
	}
}
