using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetCategories();
        IEnumerable<CategoryModel> GetCategoryBySearch(string search);
        CategoryModel GetCategory(int id);

        CategoryModel PostCategory(CategoryRequest category);
        CategoryModel PutCategory(int id, CategoryRequest category);
        void DeleteCategory(int id);
    }
}
