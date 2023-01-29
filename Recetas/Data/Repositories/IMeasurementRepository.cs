using Recetas.Data.Model;

namespace Recetas.Data.Repositories
{
    public interface IMeasurementRepository
    {
        IEnumerable<MeasurementModel> GetMeasurements();
        IEnumerable<MeasurementModel> GetMeasurementBySearch(string search);
        MeasurementModel GetMeasurement(int measurementId);

        MeasurementModel PostMeasurement(MeasurementRequest measurement);
        MeasurementModel PutMeasurement(int measurementId, MeasurementRequest measurement);
        void DeleteMeasurement(int measurementId);
    }
}
