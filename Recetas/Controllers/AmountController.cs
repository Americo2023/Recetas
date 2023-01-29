using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmountController : ControllerBase
    {
        private readonly IAmountRepository _dataRepository;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;

        public AmountController(
            IAmountRepository dataRepository,
            IMeasurementRepository measurementRepository,
            IIngredientRepository ingredientRepository,
            IRecipeRepository recipeRepository)
        {
            _dataRepository = dataRepository;
            _measurementRepository = measurementRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public IEnumerable<AmountResponse> GetAmounts(string? search = null)
        {
            List<AmountResponse> amountResponses = new List<AmountResponse>();
            var response = _dataRepository.GetAmounts();
            var measurements = _measurementRepository.GetMeasurements();
            var ingredients = _ingredientRepository.GetIngredients();
            var recipes = _recipeRepository.GetRecipes();

            foreach (var item in response)
            {
                AmountResponse result = new AmountResponse();
                result.ingredient_amount = item.ingredient_amount;
                result.measurement_name = measurements.FirstOrDefault(m => m.measurement_id == item.ingredient_measurement_id).measurement_name;
                result.ingerdient_name = ingredients.FirstOrDefault(i => i.ingredient_id == item.ingredient_id).ingredient_name;
                result.recipe_name = recipes.FirstOrDefault(r => r.recipe_id == item.recipe_id).recipe_name;
                amountResponses.Add(result);
            }

            return amountResponses;


        }

        [HttpGet]
        [Route("amountsbyrecipeid/{recipe_id}")]
        public List<AmountResponse> AmountsByRecipeId(int recipe_id)
        {
            //List<AmountResponse> amountResponses = new List<AmountResponse>();
            var ingredients = _ingredientRepository.GetIngredients();
            var measurements = _measurementRepository.GetMeasurements();
            var recipes = _recipeRepository.GetRecipes();

            IEnumerable<AmountModel> amounts = _dataRepository.GetAmounts();

            List<AmountModel> amountResponses = amounts.Where(a => a.recipe_id == recipe_id).ToList();

            List<AmountResponse> response = new List<AmountResponse>();

            foreach (var amount in amountResponses)
            {
                AmountResponse resposne = new AmountResponse();
                resposne.recipe_name = recipes.FirstOrDefault(r => r.recipe_id == amount.recipe_id).recipe_name;
                resposne.measurement_name = measurements.FirstOrDefault(m => m.measurement_id == amount.ingredient_measurement_id).measurement_name;
                resposne.ingerdient_name = ingredients.FirstOrDefault(i => i.ingredient_id == amount.ingredient_id).ingredient_name;
                resposne.ingredient_amount = amount.ingredient_amount;

                response.Add(resposne);
            }



            return response;
        }

        [HttpGet("{amountId}")]
        public ActionResult<AmountResponse> GetAmount(int amountId)
        {
            var amount = _dataRepository.GetAmount(amountId);
            var measurements = _measurementRepository.GetMeasurements();
            var ingredients = _ingredientRepository.GetIngredients();
            var recipes = _recipeRepository.GetRecipes();

            AmountResponse result = new AmountResponse();
            result.ingredient_amount = amount.ingredient_amount;
            result.measurement_name = measurements.FirstOrDefault(m => m.measurement_id == amount.ingredient_measurement_id).measurement_name;
            result.ingerdient_name = ingredients.FirstOrDefault(i => i.ingredient_id == amount.ingredient_id).ingredient_name;
            result.recipe_name = recipes.FirstOrDefault(r => r.recipe_id == amount.recipe_id).recipe_name;


            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public ActionResult<AmountModel> PostAmount(AmountRequest amountRequest)
        {
            var savedAmount = _dataRepository.PostAmount(amountRequest);
            return CreatedAtAction(nameof(GetAmount),
                new { quantity_id = savedAmount.amount_id }, savedAmount);
        }

        [HttpPut("{amountId}")]
        public ActionResult<AmountModel> PutAmount(int amountId, [FromBody] AmountRequest amountRequest)
        {
            var amount = _dataRepository.GetAmount(amountId);

            if (amount == null)
            {
                return NotFound();
            }

            amountRequest.recipe_id =
                amountRequest.recipe_id == null ?
                    amount.recipe_id : amountRequest.recipe_id;
            amountRequest.ingredient_id =
                amountRequest.ingredient_id == null ?
                    amount.ingredient_id : amountRequest.ingredient_id;
            amountRequest.ingredient_measurement_id =
                amountRequest.ingredient_measurement_id == null ?
                    amount.ingredient_measurement_id : amountRequest.ingredient_measurement_id;
            amountRequest.ingredient_amount =
                amountRequest.ingredient_amount == null ?
                    amount.ingredient_amount : amountRequest.ingredient_amount;

            var savedAmount = _dataRepository.PutAmount(amountId, amountRequest);
            return savedAmount;
        }

        //[HttpDelete("{amountId}")]
        //public ActionResult DeleteAmount(int amountId)
        //{
        //    var amount = _dataRepository.GetAmount(amountId);
        //    if (amount == null)
        //    {
        //        return NotFound();
        //    }

        //    _dataRepository.DeleteAmount(amountId);
        //    return NoContent();
        //}
    }
}
