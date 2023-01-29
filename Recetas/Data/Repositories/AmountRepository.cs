using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class AmountRepository : IAmountRepository
    {
        private readonly string _connectionString;

        public AmountRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        //public void DeleteAmount(int amountId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        connection.Execute(
        //            @"EXEC dbo.Amount_Delete 
        //                @amount_id = @amount_id",
        //            new { @amount_id = amountId }
        //        );

        //    }
        //}

        public IEnumerable<AmountModel> GetAmounts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var test = connection.Query<AmountModel>(
                    @"EXEC dbo.Amount_GetAll");
                return test;
            }
        }

        public AmountModel GetAmount(int amountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var quantity = connection.QueryFirstOrDefault<AmountModel>(
                    @"EXEC dbo.Amount_GetSingle @amount_id = @amount_id",
                    new { amount_id = amountId }
                );

                return quantity;
            }
        }

        //public IEnumerable<AmountModel> GetAmountBySearch(string search)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        return connection.Query<AmountModel>(
        //            @"EXEC dbo.Amount_GetBySearch @Search = @Search",
        //            new { Search = search }
        //        );
        //    }
        //}

        public AmountModel PostAmount(AmountRequest amountRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var amountId = connection.QueryFirst<int>(
                    @"EXEC dbo.Amount_Post 
                        @recipe_id = @recipe_id,
                        @ingredient_id = @ingredient_id,
                        @ingredient_measurement_id = @ingredient_measurement_id,
                        @ingredient_amount = @ingredient_amount",
                    amountRequest
                );


                return GetAmount(amountId);
            }
        }

        public AmountModel PutAmount(int amountId, AmountRequest amountRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Amount_Put
                        @amount_id = @amount_id,
                        @recipe_id = @recipe_id,
                        @ingredient_id = @ingredient_id,
                        @ingredient_measurement_id = @ingredient_measurement_id,
                        @ingredient_amount = @ingredient_amount",
                    new
                    {
                        @amount_id = amountId,
                        amountRequest.recipe_id,
                        amountRequest.ingredient_id,
                        amountRequest.ingredient_measurement_id,
                        amountRequest.ingredient_amount
                    });

                return GetAmount(amountId);
            }
        }
    }
}
