using System.Collections.Generic;
using Pipliz.JSON;
using System.Linq;

namespace Pipliz.APIProvider.Recipes
{
	public class RecipeManager
	{
		public static Dictionary<string, Recipe[]> RecipeStorage = new Dictionary<string, Recipe[]>();
		public static Dictionary<string, RecipeFueled[]> RecipeFueledStorage = new Dictionary<string, RecipeFueled[]>();

		public static List<Recipe> LoadRecipes (string path)
		{
			List<Recipe> recipes = new List<Recipe>();
			JSONNode node = JSON.JSON.Deserialize(path);
			foreach (var obj in node.LoopArray()) {
				recipes.Add(new Recipe(obj));
			}
			return recipes;
		}

		public static void LoadRecipes (string name, string path)
		{
			AddRecipes(name, LoadRecipes(path));
		}

		public static void AddRecipes (string name, List<Recipe> newRecipes)
		{
			Recipe[] recipes;
			if (RecipeStorage.TryGetValue(name, out recipes)) {
				RecipeStorage[name] = recipes
					.AsEnumerable()
					.Concat(newRecipes)
					.ToArray();
			} else {
				RecipeStorage[name] = newRecipes.ToArray();
			}
		}

		public static List<RecipeFueled> LoadRecipesFueled (string path)
		{
			List<RecipeFueled> recipes = new List<RecipeFueled>();
			JSONNode node = JSON.JSON.Deserialize(path);
			foreach (var obj in node.LoopArray()) {
				recipes.Add (new RecipeFueled(obj));
			}
			return recipes;
		}

		public static void LoadRecipesFueled (string name, string path)
		{
			AddRecipesFueled(name, LoadRecipesFueled(path));
		}

		public static void AddRecipesFueled (string name, List<RecipeFueled> newRecipes)
		{
			RecipeFueled[] recipes;
			if (RecipeFueledStorage.TryGetValue(name, out recipes)) {
				RecipeFueledStorage[name] = recipes
					.AsEnumerable()
					.Concat(newRecipes)
					.ToArray();
			} else {
				RecipeFueledStorage[name] = newRecipes.ToArray();
			}
		}
	}
}
