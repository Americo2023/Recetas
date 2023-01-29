using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface IMaltidRepository
    {
        IEnumerable<MaltidModel> GetMaltider();
        IEnumerable<MaltidModel> GetMaltidBySearch(string search);
        MaltidModel GetMaltid(int maltidId);

        MaltidModel PostMaltid(MaltidRequest maltid);
        MaltidModel PutMaltid(int maltid_id, MaltidRequest maltid);
        void DeleteMaltid(int maltidId);
    }
}
