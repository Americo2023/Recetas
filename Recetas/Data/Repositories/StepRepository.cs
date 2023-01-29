using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class StepRepository: IStepRepository
    {
        private readonly string _connectionString;

        public StepRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }


        public void DeleteStep(int stepId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Step_Delete 
                        @step_id = @step_id",
                    new { @step_id = stepId }
                );

            }
        }

        public IEnumerable<StepModel> GetSteps()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<StepModel>(
                    @"EXEC dbo.Step_GetAll");
            }
        }

        public StepModel GetStep(int stepId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var step = connection.QueryFirstOrDefault<StepModel>(
                    @"EXEC dbo.Step_GetSingle @step_id = @step_id",
                    new { step_id = stepId }
                );

                return step;
            }
        }

        public IEnumerable<StepModel> GetStepBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<StepModel>(
                    @"EXEC dbo.Step_GetBySearch @Search = @Search",
                    new { Search = search }
                );
            }
        }

        public IEnumerable<StepModel> GetStepByRecipeId(int recipe_id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<StepModel>(
                    @"EXEC dbo.Step_GetByRecipeId @recipe_id = @recipe_id",
                    new { recipe_id = recipe_id }
                );
            }
        }

        public StepModel PostStep(StepRequest stepRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var stepId = connection.QueryFirst<int>(
                    @"EXEC dbo.Step_Post 
                        @recipe_id = @recipe_id,
                        @step_number = @step_number,
                        @step_description = @step_description",
                    stepRequest
                );


                return GetStep(stepId);
            }
        }

        public StepModel PutStep(int stepId, StepRequest stepRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(

                    @"EXEC dbo.Course_Put
                        @step_id = @step_id,
                        @recipe_id = @recipe_id,
                        @step_number = @step_number,
                        @step_description = @step_description",
                    new
                    {
                        @step_id = stepId,
                        stepRequest.recipe_id,
                        stepRequest.step_number,
                        stepRequest.step_description
                    });

                return GetStep(stepId);
            }
        }
    }
}
