using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface IStepRepository
    {
        IEnumerable<StepModel> GetSteps();
        IEnumerable<StepModel> GetStepBySearch(string search);
        IEnumerable<StepModel> GetStepByRecipeId(int search);
        StepModel GetStep(int stepId);

        StepModel PostStep(StepRequest stepRequest);
        StepModel PutStep(int stepId, StepRequest stepRequest);
        void DeleteStep(int stepId);
    }
}
