using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Consts;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.DTOs.RegionDTOs;
using RentVilla.Application.Enums;
using RentVilla.Application.Feature.Commands.StateImages.DeleteStateImages;
using RentVilla.Application.Feature.Commands.StateImages.UploadStateImages;
using RentVilla.Application.Feature.Queries.Region.GetAllStates;
using RentVilla.Application.Feature.Queries.StateImages.GetStateImages;
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
        private readonly IMediator _mediator;

        public RegionController(IDistrictReadRepository districtReadRepository, IStateReadRepository stateReadRepository, ICityReadRepository cityReadRepository, IMediator mediator)
        {
            _districtReadRepository = districtReadRepository;
            _stateReadRepository = stateReadRepository;
            _cityReadRepository = cityReadRepository;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStates([FromQuery] GetAllStatesQueryRequest getAllStatesQueryRequest)
        {
            GetAllStatesQueryResponse response = await _mediator.Send(getAllStatesQueryRequest);
            return Ok(response.StateDTOs);
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
        [HttpGet]
        public async Task<IActionResult> GetStateImages(GetStateImagesQueryRequest getStateImagesQueryRequest)
        {
            var response = await _mediator.Send(getStateImagesQueryRequest);
            return Ok(response.imageFiles);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Regions, Definition = "Uploads state image", ActionType = ActionTypes.Writing)]
        public async Task<IActionResult> UploadStateImage([FromQuery] UploadStateImagesCommandRequest uploadStateImagesCommandRequest)
        {
            var response = await _mediator.Send(uploadStateImagesCommandRequest);
            return Ok(response);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Regions, Definition = "Deletes state image", ActionType = ActionTypes.Deleting)]
        public async Task<IActionResult> DeleteStateImage([FromBody] DeleteStateImagesCommandRequest deleteStateImagesCommandRequest)
        {
            var response = await _mediator.Send(deleteStateImagesCommandRequest);
            return Ok(response);
        }
    }
}
