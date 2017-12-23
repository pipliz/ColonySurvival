using System.Collections.Generic;

namespace Pipliz.Mods.APIProvider.Jobs
{
	public interface IRecipeLimitsProvider
	{
		IList<Recipe> GetCraftingLimitsRecipes ();

		List<string> GetCraftingLimitsTriggers ();

		string GetCraftingLimitsType ();
	}
}
