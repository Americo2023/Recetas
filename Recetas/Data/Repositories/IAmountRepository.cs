using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface IAmountRepository
    {
        IEnumerable<AmountModel> GetAmounts();
        AmountModel GetAmount(int amountId);
        AmountModel PostAmount(AmountRequest amount);
        AmountModel PutAmount(int amountId, AmountRequest amount);
    }
}
