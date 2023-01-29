using Dapper;
using Microsoft.Data.SqlClient;
using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public class MaltidRepository : IMaltidRepository
    {
        private readonly string _connectionString;

        public MaltidRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public void DeleteMaltid(int maltidId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Maltid_Delete 
                        @maltid_id = @maltid_id",
                    new { @maltid_id = maltidId }
                );

            }
        }

        public MaltidModel GetMaltid(int maltidId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var maltid = connection.QueryFirstOrDefault<MaltidModel>(
                    @"EXEC dbo.Maltid_GetSingle @maltid_id = @maltid_id",
                    new { maltid_id = maltidId }
                );

                return maltid;
            }
        }

        public IEnumerable<MaltidModel> GetMaltidBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<MaltidModel>(
                    @"EXEC dbo.Maltid_GetBySearch @Search = @Search",
                    new { Search = search }
                );
            }
        }

        public IEnumerable<MaltidModel> GetMaltider()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<MaltidModel>(
                    @"EXEC dbo.Maltid_GetAll");
            }
        }

        public MaltidModel PostMaltid(MaltidRequest maltid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var maltidId = connection.QueryFirst<int>(
                    @"EXEC dbo.Maltid_Post 
                        @maltid_name = @maltid_name,
                        @maltid_img = @maltid_img",
                    maltid
                );

                return GetMaltid(maltidId);
            }
        }

        public MaltidModel PutMaltid(int maltid_id, MaltidRequest maltid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(
                    @"EXEC dbo.Maltid_Put
                        @maltid_id = @maltid_id, 
                        @maltid_name = @maltid_name,
                        @maltid_img = @maltid_img",
                    new
                    {
                        @maltid_id = maltid_id,
                        maltid.maltid_name,
                        maltid.maltid_img
                    });

                return GetMaltid(maltid_id);
            }
        }
    }
}
