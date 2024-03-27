using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.RegionRepo;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly IStateReadRepository _stateReadRepository;
        private readonly ICityReadRepository _cityReadRepository;

        public RegionController(IDistrictReadRepository districtReadRepository, IStateReadRepository stateReadRepository, ICityReadRepository cityReadRepository)
        {
            _districtReadRepository = districtReadRepository;
            _stateReadRepository = stateReadRepository;
            _cityReadRepository = cityReadRepository;
        }
        [HttpGet]
        public IActionResult GetAllStates()
        {
            var states = _stateReadRepository.GetAllList();
            return Ok(states);
        }
        [HttpGet]
        public IActionResult GetAllDistricts(string cityId)
        {
            var districts = _districtReadRepository.GetWhere(string.IsNullOrEmpty(cityId) ? null : x => x.CityId.ToString() == cityId);
            return Ok(districts);
        }
        [HttpGet]
        public IActionResult GetAllCities(string stateId)
        {
            var cities = _cityReadRepository.GetWhere(string.IsNullOrEmpty(stateId) ? null : x => x.StateId.ToString() == stateId);
            return Ok(cities);
        }
    }
}
