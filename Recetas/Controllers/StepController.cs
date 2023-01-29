using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IStepRepository _dataRepository;

        public StepController(IStepRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<StepModel> GetSteps(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetSteps();
            }
            else
            {
                return _dataRepository.GetStepBySearch(search);
            }
        }

        [HttpGet]
        [Route("getstepbyrecipeid/{recipe_id}")]
        public IEnumerable<StepModel> GetStepByRecipeId(int recipe_id)
        {
            IEnumerable<StepModel> steps = _dataRepository.GetStepByRecipeId(recipe_id);
            return steps;
        }

        [HttpGet("{stepId}")]
        public ActionResult<StepModel> GetStep(int stepId)
        {
            var step = _dataRepository.GetStep(stepId);

            if (step == null)
            {
                return NotFound();
            }
            return step;
        }

        [HttpPost]
        public ActionResult<StepModel> PostStep(StepRequest stepRequest)
        {
            var savedStep = _dataRepository.PostStep(stepRequest);
            return CreatedAtAction(nameof(GetStep),
                new { step_id = savedStep.step_id }, savedStep);
        }

        [HttpPut("{stepId}")]
        public ActionResult<StepModel> PutStep(int stepId, StepRequest stepRequest)
        {
            var step = _dataRepository.GetStep(stepId);

            if (step == null)
            {
                return NotFound();
            }


            stepRequest.recipe_id = stepRequest.recipe_id == null ? step.recipe_id : stepRequest.recipe_id;
            stepRequest.step_number = stepRequest.step_number == null ? step.step_number : stepRequest.step_number;
            stepRequest.step_description = string.IsNullOrEmpty(stepRequest.step_description) ? step.step_description : stepRequest.step_description;

            var savedStep = _dataRepository.PutStep(stepId, stepRequest);
            return savedStep;
        }

        [HttpDelete("{stepId}")]
        public ActionResult DeleteRecepie(int stepId)
        {
            var step = _dataRepository.GetStep(stepId);
            if (step == null)
            {
                return NotFound();
            }

            _dataRepository.DeleteStep(stepId);
            return NoContent();
        }
    }
}
