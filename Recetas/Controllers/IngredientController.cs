using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _dataRepository;

        public IngredientController(IIngredientRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<IngredientModel> GetIngredients(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetIngredients();
            }
            else
            {
                return _dataRepository.GetIngredientBySearch(search);
            }
        }

        [HttpGet("{ingredientId}")]
        public ActionResult<IngredientModel> GetIngredient(int ingredientId)
        {
            var ingredient = _dataRepository.GetIngredient(ingredientId);

            if (ingredient == null)
            {
                return NotFound();
            }
            return ingredient;
        }

        [HttpPost]
        public ActionResult<IngredientModel> PostIngredient(IngredientRequest ingredientRequest)
        {
            var savedIngredient = _dataRepository.PostIngredient(ingredientRequest);
            return CreatedAtAction(nameof(GetIngredient),
                new { ingredient_id = savedIngredient.ingredient_id }, savedIngredient);
        }

        [HttpPut("{ingredientId}")]
        public ActionResult<IngredientModel> PutIngredient(int ingredientId, IngredientRequest ingredientRequest)
        {
            var ingredient = _dataRepository.GetIngredient(ingredientId);

            if (ingredient == null)
            {
                return NotFound();
            }

            ingredientRequest.ingredient_name =
                string.IsNullOrEmpty(ingredientRequest.ingredient_name) ?
                    ingredient.ingredient_name : ingredientRequest.ingredient_name;

            var savedIngredient = _dataRepository.PutIngredient(ingredientId, ingredientRequest);
            return savedIngredient;
        }

        [HttpDelete("{ingredientId}")]
        public ActionResult DeleteIngredient(int ingredientId)
        {
            var ingredient = _dataRepository.GetIngredient(ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }

            _dataRepository.DeleteIngredient(ingredientId);
            return NoContent();
        }
    }
}
