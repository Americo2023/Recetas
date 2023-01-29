using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface IRecipeRepository
    {
        IEnumerable<RecipeModel> GetRecipes();
        IEnumerable<RecipeModel> GetRecipesBySearch(string search);
        RecipeModel GetRecipe(int recipeId);
        //IEnumerable<RecipeModel> GetRecipesByCategory(int categoryId);
        bool RecipeExists(int recipeId);
        RecipeModel PostRecipe(RecipeRequest recepie);
        RecipeModel PutRecipe(int recepieId, RecipeRequest recepie);
        void DeleteRecipe(int recepieId);
    }
}
