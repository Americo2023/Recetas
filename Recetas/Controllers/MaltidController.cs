using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recetas.Data.Model;
using Recetas.Data.Repositories;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaltidController : ControllerBase
    {
        private readonly IMaltidRepository _dataRepository;

        public MaltidController(IMaltidRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<MaltidModel> GetMaltider(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetMaltider();
            }
            else
            {
                return _dataRepository.GetMaltidBySearch(search);
            }
        }

        [HttpGet("{maltid_id}")]
        public ActionResult<MaltidModel> GetMaltid(int maltid_id)
        {
            var maltid = _dataRepository.GetMaltid(maltid_id);

            if (maltid == null)
            {
                return NotFound();
            }
            return maltid;
        }

        [HttpPost]
        public ActionResult<MaltidModel> PostMaltid(MaltidRequest maltidRequest)
        {
            var savedMaltid = _dataRepository.PostMaltid(maltidRequest);
            return CreatedAtAction(nameof(GetMaltid),
                new { maltid_id = savedMaltid.maltid_id }, savedMaltid);
        }

        [HttpPut("{maltid_id}")]
        public ActionResult<MaltidModel> PutMaltid(int maltid_id, MaltidRequest maltidRequest)
        {
            var maltid = _dataRepository.GetMaltid(maltid_id);

            if (maltid == null)
            {
                return NotFound();
            }

            maltidRequest.maltid_name =
                string.IsNullOrEmpty(maltidRequest.maltid_name) ?
                    maltid.maltid_name : maltidRequest.maltid_name;

            var savedMaltid = _dataRepository.PutMaltid(maltid_id, maltidRequest);
            return savedMaltid;
        }

        [HttpDelete("{maltid_id}")]
        public ActionResult DeleteRecepie(int maltid_id)
        {
            var maltid = _dataRepository.GetMaltid(maltid_id);
            if (maltid == null)
            {
                return NotFound();
            }

            _dataRepository.DeleteMaltid(maltid_id);
            return NoContent();
        }
    }
}
