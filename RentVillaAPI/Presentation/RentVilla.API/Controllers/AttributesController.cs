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
        public async Task<IActionResult> Get()
        {
            var models = _attributeReadRepository.GetAttributes();
            return Ok(models);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _attributeReadRepository.GetByIdAsync(id, false);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AttributeCreateVM model)
        {
                var attributeType = await _attributeTypeReadRepository.GetSingleAsync(x => x.Name == model.Name);

                if (attributeType == null)
                {
                    attributeType = new AttributeType { Name = model.Name };
                }
                attributeType.Attributes.Add(new Attributes
                {
                    Description = model.Description,
                    IsActive = model.IsActive,
                    AttributeType = attributeType
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
