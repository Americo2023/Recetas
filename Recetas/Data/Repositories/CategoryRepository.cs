using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public void DeleteCategory(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Category_Delete 
                        @category_id = @category_id",
                    new { @category_id = id }
                );

            }
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<CategoryModel>(
                    @"EXEC dbo.Category_GetAll");
            }
        }

        public CategoryModel GetCategory(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var category = connection.QueryFirstOrDefault<CategoryModel>(
                    @"EXEC dbo.Category_GetSingle @category_id = @category_id",
                    new { category_id = id }
                );

                return category;
            }
        }

        public IEnumerable<CategoryModel> GetCategoryBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<CategoryModel>(
                    @"EXEC dbo.Category_GetBySearch @Search = @Search",
                    new { Search = search }
                );
            }
        }

        public CategoryModel PostCategory(CategoryRequest categoryRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var category_id = connection.QueryFirst<int>(
                    @"EXEC dbo.Category_Post 
                        @category_name = @category_name,
                        @category_img = @category_img",
                    categoryRequest
                );

                return GetCategory(category_id);
            }
        }

        public CategoryModel PutCategory(int id, CategoryRequest categoryRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Category_Put
                        @category_id = @category_id, 
                        @category_name = @category_name,            
                        @category_img = @category_img",
                    new
                    {
                        @category_id = id,
                        categoryRequest.category_name
                    });

                return GetCategory(id);
            }
        }
    }
}
