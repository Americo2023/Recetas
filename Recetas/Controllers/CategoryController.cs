using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _dataRepository;

        public CategoryController(ICategoryRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<CategoryModel> GetCategories(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetCategories();
            }
            else
            {
                return _dataRepository.GetCategoryBySearch(search);
            }
        }

        [HttpGet("{category_id}")]
        public ActionResult<CategoryModel> GetCategory(int category_id)
        {
            var category = _dataRepository.GetCategory(category_id);

            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public ActionResult<CategoryModel> PostCategory(CategoryRequest categoryRequest)
        {
            var savedCat = _dataRepository.PostCategory(categoryRequest);
            return CreatedAtAction(nameof(GetCategory),
                new { category_id = savedCat.category_id }, savedCat);
        }

        [HttpPut("{category_id}")]
        public ActionResult<CategoryModel> PutCategory(int category_id, CategoryRequest categoryRequest)
        {
            var cat = _dataRepository.GetCategory(category_id);

            if (cat == null)
            {
                return NotFound();
            }

            categoryRequest.category_name =
                string.IsNullOrEmpty(categoryRequest.category_name) ?
                    cat.category_name : categoryRequest.category_name;
            categoryRequest.category_img =
                string.IsNullOrEmpty(categoryRequest.category_img) ?
                    cat.category_img : categoryRequest.category_img;

            var savedCat = _dataRepository.PutCategory(category_id, categoryRequest);
            return savedCat;
        }

        [HttpDelete("{category_id}")]
        public ActionResult DeleteCategory(int category_id)
        {
            var cat = _dataRepository.GetCategory(category_id);
            if (cat == null)
            {
                return NotFound();
            }

            _dataRepository.DeleteCategory(category_id);
            return NoContent();
        }
    }
}
