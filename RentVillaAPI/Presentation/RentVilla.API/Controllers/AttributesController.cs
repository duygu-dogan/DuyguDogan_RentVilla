using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.ViewModels.Attribute;
using RentVilla.Application.ViewModels.Product;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Attribute;
using System.Net;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeReadRepository _attributeReadRepository;
        private readonly IAttributeWriteRepository _attributeWriteRepository;
        private readonly IAttributeTypeReadRepository _attributeTypeReadRepository;
        private readonly IAttributeTypeWriteRepository _attributeTypeWriteRepository;

        public AttributesController(IAttributeReadRepository attributeReadRepository, IAttributeWriteRepository attributeWriteRepository, IAttributeTypeReadRepository attributeTypeReadRepository, IAttributeTypeWriteRepository attributeTypeWriteRepository)
        {
            _attributeReadRepository = attributeReadRepository;
            _attributeWriteRepository = attributeWriteRepository;
            _attributeTypeReadRepository = attributeTypeReadRepository;
            _attributeTypeWriteRepository = attributeTypeWriteRepository;
        }

        [HttpGet]
        public IActionResult GetAttributeTypes()
        {
            var models = _attributeTypeReadRepository.GetAll();
            return Ok(models);
        }
        [HttpGet]
        public async Task<IActionResult> GetAttributeTypeById(string id)
        {
            var attributeType = await _attributeTypeReadRepository.GetByIdAsync(id, false);
            if (attributeType == null)
            {
                return NotFound();
            }
            return Ok(attributeType);
        }
        [HttpPost]
        public async Task<IActionResult> AddAttributeTypeAsync(string name)
        {
            var AttributeType = await _attributeTypeWriteRepository.AddAsync(new AttributeType { Name = name });
            await _attributeTypeWriteRepository.SaveAsync();
            return Ok(AttributeType);

        }
        [HttpGet]
        public async Task<IActionResult> GetAttributes()
        {
            var models = _attributeReadRepository.GetAttributes();
            return Ok(models);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeById(string id)
        {
            var attribute = await _attributeReadRepository.GetByIdAsync(id, false);
            if (attribute == null)
            {
                return NotFound();
            }
            return Ok(attribute);
        }
        [HttpPost]
        public async Task<IActionResult> AddAttributeAsync(AttributeCreateVM model)
        {
                var attribute = await _attributeWriteRepository.AddAsync(new Attributes
                {
                    Description = model.Description,
                    IsActive = model.IsActive,
                    AttributeType = await _attributeTypeReadRepository.GetByIdAsync(model.AttributeTypeId),
                });
                await _attributeWriteRepository.SaveAsync();
                return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update(AttributeUpdateVM model)
        {
            Attributes attributes = await _attributeReadRepository.GetByIdAsync(model.Id);
            AttributeType attributeType = attributes.AttributeType;
            attributeType.Name = model.Name;
            attributes.Description = model.Description;
            attributes.IsActive = model.IsActive;
            await _attributeWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _attributeWriteRepository.DeleteAsync(id);
            await _attributeWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
