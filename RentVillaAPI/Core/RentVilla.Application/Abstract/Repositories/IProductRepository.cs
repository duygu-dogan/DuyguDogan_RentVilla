using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstract.Repositories
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetProductWithAttributesAsync(Guid ProductId);
        Task<List<Product>> GetProductsByAttributeIdAsync(Guid AttributeId);

    }
}
