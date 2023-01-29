using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _dataRepository;
        private readonly IStepRepository _stepRepository;

        public RecipesController(IRecipeRepository dataRepository, IStepRepository stepRepository)
        {
            _dataRepository = dataRepository;
            _stepRepository = stepRepository;
        }

        [HttpGet]
        public IEnumerable<RecipeModel> GetRecipes(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetRecipes();
            }
            else
            {
                return _dataRepository.GetRecipesBySearch(search);
            }
        }

        [HttpGet]
        [Route("getrecipesbycategory/{category_id}")]
        public IEnumerable<RecipeModel> GetRecipesByCategory(int category_id)
        {
            IEnumerable<RecipeModel> recipes = _dataRepository.GetRecipes();
            return recipes.Where(r => r.category_id == category_id);
        }

        [HttpGet("{recipe_id}")]
        public ActionResult<RecipeModel> GetRecipe(int recipe_id)
        {
            var recipe = _dataRepository.GetRecipe(recipe_id);
            IEnumerable<StepModel> steps = _stepRepository.GetSteps();


            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        [HttpPost]
        public ActionResult<RecipeModel> Postrecipe(RecipeRequest recipeRequest)
        {
            var savedRecipe = _dataRepository.PostRecipe(recipeRequest);
            return CreatedAtAction(nameof(GetRecipe),
                new { recipe_id = savedRecipe.recipe_id }, savedRecipe);
        }

        [HttpPut("{recipe_id}")]
        public ActionResult<RecipeModel> PutRecipe(int recipe_id, RecipeRequest recipeRequest)
        {
            var recipe = _dataRepository.GetRecipe(recipe_id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipeRequest.maltid_id =
                recipeRequest.maltid_id == null ?
                    recipe.maltid_id : recipeRequest.maltid_id;
            recipeRequest.category_id =
                recipeRequest.category_id == null ?
                    recipe.category_id : recipeRequest.category_id;
            recipeRequest.recipe_name =
                string.IsNullOrEmpty(recipeRequest.recipe_name) ?
                    recipe.recipe_name : recipeRequest.recipe_name;
            recipeRequest.recipe_description =
                string.IsNullOrEmpty(recipeRequest.recipe_description) ?
                    recipe.recipe_description : recipeRequest.recipe_description;
            recipeRequest.prep_time =
                string.IsNullOrEmpty(recipeRequest.prep_time) ?
                    recipe.prep_time : recipeRequest.prep_time;
            recipeRequest.recipe_img =
                string.IsNullOrEmpty(recipeRequest.recipe_img) ?
                    recipe.recipe_img : recipeRequest.recipe_img;

            var savedrecipe = _dataRepository.PutRecipe(recipe_id, recipeRequest);
            return savedrecipe;
        }

        [HttpDelete("{recipe_id}")]
        public ActionResult Deleterecipe(int recipe_id)
        {
            var recipe = _dataRepository.GetRecipe(recipe_id);
            if (recipe == null)
            {
                return NotFound();
            }

            _dataRepository.DeleteRecipe(recipe_id);
            return NoContent();
        }
    }
}
