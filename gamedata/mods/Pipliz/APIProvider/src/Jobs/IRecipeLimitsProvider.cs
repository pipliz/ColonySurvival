using System.Collections.Generic;

namespace Pipliz.APIProvider.Jobs
{
	public interface IRecipeLimitsProvider
	{
		Recipe[] GetCraftingLimitsRecipes ();

		List<string> GetCraftingLimitsTriggers ();

		string GetCraftingLimitsIdentifier ();
	}
}
