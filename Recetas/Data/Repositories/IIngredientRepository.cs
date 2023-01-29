using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientModel> GetIngredients();
        IEnumerable<IngredientModel> GetIngredientBySearch(string search);
        IngredientModel GetIngredient(int ingredientId);

        IngredientModel PostIngredient(IngredientRequest ingredient);
        IngredientModel PutIngredient(int ingredientId, IngredientRequest ingredient);
        void DeleteIngredient(int ingredientId);
    }
}
