using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class RecipeRepository: IRecipeRepository
    {
        private readonly string _connectionString;

        public RecipeRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public void DeleteRecipe(int recipe_id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Recipe_Delete 
                        @recipe_id = @recipe_id",
                    new { @recipe_id = recipe_id }
                );

            }
        }

        public RecipeModel GetRecipe(int recipe_id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var recipe = connection.QueryFirstOrDefault<RecipeModel>(
                    @"EXEC dbo.Recipe_GetSingle @recipe_id = @recipe_id",
                    new { recipe_id = recipe_id }
                );

                return recipe;
            }
        }

        public IEnumerable<RecipeModel> GetRecipes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<RecipeModel>(
                    @"EXEC dbo.Recipe_GetAll");
            }
        }

        public IEnumerable<RecipeModel> GetRecipesBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<RecipeModel>(
                    @"EXEC dbo.Recipe_GetBySearch @Search = @Search",
                    new { Search = search }
                );
            }
        }

        //public IEnumerable<RecipeModel> GetRecipesByCategory(int categoryId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        return connection.Query<RecipeModel>(
        //            @"EXEC dbo.Recipe_GetByCategory @category_id = @category_id",
        //            new { category_id = categoryId }
        //        );
        //    }
        //}

        public RecipeModel PostRecipe(RecipeRequest recepieRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var recipe_id = connection.QueryFirst<int>(
                    @"EXEC dbo.Recipe_Post 
                        @maltid_id = @maltid_id, 
                        @category_id = @category_id, 
                        @recipe_name = @recipe_name, 
                        @recipe_description = @recipe_description, 
                        @prep_time = @prep_time,
                        @recipe_img = @recipe_img",
                    recepieRequest
                );

                return GetRecipe(recipe_id);
            }
        }

        public RecipeModel PutRecipe(int recipe_id, RecipeRequest recepieRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Recipe_Put
                        @recipe_id = @recipe_id,
                        @maltid_id = @maltid_id, 
                        @category_id = @category_id, 
                        @recipe_name = @recipe_name, 
                        @recipe_description = @recipe_description, 
                        @prep_time = @prep_time,
                        @recipe_img = @recipe_img",
                    new
                    {
                        @recipe_id = recipe_id,
                        recepieRequest.maltid_id,
                        recepieRequest.category_id,
                        recepieRequest.recipe_name,
                        recepieRequest.recipe_description,
                        recepieRequest.prep_time,
                        recepieRequest.recipe_img
                    });

                return GetRecipe(recipe_id);
            }
        }

        public bool RecipeExists(int recipe_id)
        {
            throw new NotImplementedException();
        }
    }
}
