using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class MeasurementRepository: IMeasurementRepository
    {
        private readonly string _connectionString;

        public MeasurementRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public void DeleteMeasurement(int measurementId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Measurement_Delete 
                        @measurement_id = @measurement_id",
                    new { @measurement_id = measurementId }
                );

            }
        }

        public MeasurementModel GetMeasurement(int measurementId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var measurement = connection.QueryFirstOrDefault<MeasurementModel>(
                    @"EXEC dbo.Measurement_GetSingle @measurement_id = @measurement_id",
                    new { measurement_id = measurementId }
                );

                return measurement;
            }
        }

        public IEnumerable<MeasurementModel> GetMeasurementBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<MeasurementModel>(
                    @"EXEC dbo.Measurement_GetBySearch @Search = @Search",
                    new { Search = search }
                );
            }
        }

        public IEnumerable<MeasurementModel> GetMeasurements()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<MeasurementModel>(
                    @"EXEC dbo.Measurement_GetAll");
            }
        }

        public MeasurementModel PostMeasurement(MeasurementRequest measurementRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var measurementId = connection.QueryFirst<int>(
                    @"EXEC dbo.Measurement_Post 
                        @measurement_name = @measurement_name",
                    measurementRequest
                );

                return GetMeasurement(measurementId);
            }
        }

        public MeasurementModel PutMeasurement(int measurementId, MeasurementRequest measurementRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Measurement_Put
                        @measurement_id = @measurement_id, 
                        @measurement_name = @measurement_name",
                    new
                    {
                        @measurement_id = measurementId,
                        measurementRequest.measurement_name
                    });

                return GetMeasurement(measurementId);
            }
        }
    }
}
