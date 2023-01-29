using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementRepository _dataRepository;

        public MeasurementController(IMeasurementRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<MeasurementModel> GetMeasurements(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetMeasurements();
            }
            else
            {
                return _dataRepository.GetMeasurementBySearch(search);
            }
        }

        [HttpGet("{measurementId}")]
        public ActionResult<MeasurementModel> GetMeasurement(int measurementId)
        {
            var measurement = _dataRepository.GetMeasurement(measurementId);

            if (measurement == null)
            {
                return NotFound();
            }
            return measurement;
        }

        [HttpPost]
        public ActionResult<MeasurementModel> PostMeasurement(MeasurementRequest measurementRequest)
        {
            var savedMeasurement = _dataRepository.PostMeasurement(measurementRequest);
            return CreatedAtAction(nameof(GetMeasurement),
                new { measurement_id = savedMeasurement.measurement_id }, savedMeasurement);
        }

        [HttpPut("{measurementId}")]
        public ActionResult<MeasurementModel> PutMeasurement(int measurementId, MeasurementRequest measurementRequest)
        {
            var measurement = _dataRepository.GetMeasurement(measurementId);

            if (measurement == null)
            {
                return NotFound();
            }

            measurementRequest.measurement_name =
                string.IsNullOrEmpty(measurementRequest.measurement_name) ?
                    measurement.measurement_name : measurementRequest.measurement_name;

            var savedMeasurement = _dataRepository.PutMeasurement(measurementId, measurementRequest);
            return savedMeasurement;
        }

        [HttpDelete("{measurementId}")]
        public ActionResult DeleteMeasurement(int measurementId)
        {
            var measurement = _dataRepository.GetMeasurement(measurementId);
            if (measurement == null)
            {
                return NotFound();
            }

            _dataRepository.DeleteMeasurement(measurementId);
            return NoContent();
        }
    }
}
