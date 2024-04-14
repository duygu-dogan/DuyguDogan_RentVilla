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
        [ActionName("GetTypes")]
        public IActionResult GetAttributeTypes()
        {
            var models = _attributeTypeReadRepository.GetNonDeletedAttributeTypes();
            return Ok(models);
        }
        
        [HttpGet]
        [ActionName("GetTypeById")]
        public async Task<IActionResult> GetAttributeTypeById(string id)
        {
            var attributeType = _attributeTypeReadRepository.AppDbContext.Include(at => at.Attributes).Where(at => at.Id == Guid.Parse(id)).FirstOrDefault();
            List<AttributeReadVM> attributes = new();
            foreach (var a in attributeType.Attributes)
            {
                attributes.Add(new AttributeReadVM
                {
                    Id = a.Id.ToString(),
                    Name = a.AttributeType.Name,
                    TypeId= a.AttributeType.Id.ToString(),
                    Description = a.Description,
                    IsActive = a.IsActive
                });
            }
            AttributeTypeReadVM model = new()
            {
                Id = attributeType.Id.ToString(),
                Name = attributeType.Name,
                Attributes = attributes
            };
            if (attributeType == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpGet]
        [ActionName("GetDeletedTypes")]
        public IActionResult GetDeletedAttributeTypes()
        {
            var models = _attributeTypeReadRepository.GetDeletedAttributeTypes();
            return Ok(models);
        }
        [HttpPost]
        [ActionName("AddType")]
        public async Task<IActionResult> AddAttributeTypeAsync(string name)
        {
            var AttributeType = await _attributeTypeWriteRepository.AddAsync(new AttributeType { Name = name });
            await _attributeTypeWriteRepository.SaveAsync();
            return Ok(AttributeType);
        }
        [HttpPut]
        [ActionName("UpdateType")]
        public async Task<IActionResult> Update(AttributeTypeUpdateVM model)
        {
            var attributeType = await _attributeTypeReadRepository.GetByIdAsync(model.Id);
            attributeType.Name = model.Name;
            await _attributeTypeWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        [ActionName("SoftDeleteType")]
        public async Task<IActionResult> SoftDeleteAttributeTypeAsync(string id)
        {
            var attributType = await _attributeTypeReadRepository.GetByIdAsync(id);
            attributType.IsDeleted = !attributType.IsDeleted;
            await _attributeTypeWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete]
        [ActionName("DeleteType")]
        public async Task<IActionResult> DeleteAttributeTypeAsync(string id)
        {
            var attributType = _attributeTypeReadRepository.AppDbContext.Include(at => at.Attributes).Where(at => at.Id == Guid.Parse(id)).FirstOrDefault();
            if(attributType != null)
            {
                _attributeWriteRepository.DeleteRange(attributType.Attributes.ToList());
                await _attributeWriteRepository.SaveAsync();
                await _attributeTypeWriteRepository.DeleteAsync(id);
                await _attributeTypeWriteRepository.SaveAsync();
                return Ok();
            }else
                return NotFound();

        }

        [HttpGet]
        [ActionName("Get")]
        public IActionResult GetAttributes()
        {
            var models = _attributeReadRepository.GetAttributes();
            return Ok(models);
        }
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetAttributeById(string id)
        {
            var attribute = await _attributeReadRepository.GetByIdAsync(id, false);
            if (attribute == null)
            {
                return NotFound();
            }
            return Ok(attribute);
        }
        [HttpGet("typeId")]
        [ActionName("GetByTypeId")]
        public IActionResult GetAttributesByTypeId(string typeId)
        {
            var attributes = _attributeReadRepository.GetAttributesByTypeId(typeId);
            if (attributes == null)
            {
                return NotFound();
            }
            return Ok(attributes);
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> AddAttributeAsync(AttributeCreateVM model)
        {
                await _attributeWriteRepository.AddAsync(new Attributes
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
