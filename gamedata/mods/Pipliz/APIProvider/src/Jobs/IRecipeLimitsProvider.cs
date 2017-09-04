using System.Collections.Generic;

namespace Pipliz.APIProvider.Jobs
{
	public interface IRecipeLimitsProvider
	{
		IList<Recipe> GetCraftingLimitsRecipes ();

		List<string> GetCraftingLimitsTriggers ();

		string GetCraftingLimitsType ();
	}
}
