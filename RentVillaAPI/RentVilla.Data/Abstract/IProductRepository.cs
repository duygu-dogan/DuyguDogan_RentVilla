using RentVilla.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Data.Abstract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetProductWithAttributesAsync(Guid ProductId);
        Task<List<Product>> GetProductsByAttributeIdAsync(Guid AttributeId);

    }
}
