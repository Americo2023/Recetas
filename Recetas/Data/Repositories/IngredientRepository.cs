using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;

        public IngredientRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public void DeleteIngredient(int ingredientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Ingredient_Delete 
                        @ingredient_id = @ingredient_id",
                    new { @ingredient_id = ingredientId }
                );

            }
        }

        public IngredientModel GetIngredient(int ingredientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var ingredient = connection.QueryFirstOrDefault<IngredientModel>(
                    @"EXEC dbo.Ingredient_GetSingle @ingredient_id = @ingredient_id",
                    new { ingredient_id = ingredientId }
                );

                return ingredient;
            }
        }

        public IEnumerable<IngredientModel> GetIngredientBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<IngredientModel>(
                    @"EXEC dbo.Ingredient_GetBySearch @Search = @Search",
                    new { Search = search }
                );
            }
        }

        public IEnumerable<IngredientModel> GetIngredients()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<IngredientModel>(
                    @"EXEC dbo.Ingredient_GetAll");
            }
        }

        public IngredientModel PostIngredient(IngredientRequest ingredient)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var ingredientId = connection.QueryFirst<int>(
                    @"EXEC dbo.Ingredient_Post 
                        @ingredient_name = @ingredient_name",
                    ingredient
                );

                return GetIngredient(ingredientId);
            }
        }

        public IngredientModel PutIngredient(int ingredientId, IngredientRequest ingredient)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Ingredient_Put
                        @ingredient_id = @ingredient_id, 
                        @ingredient_name = @ingredient_name",
                    new
                    {
                        @ingredient_id = ingredientId,
                        ingredient.ingredient_name
                    });

                return GetIngredient(ingredientId);
            }
        }
    }
}
