using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RentVilla.Data.Abstract;
using RentVilla.Data.Concrete.Contexts;
using RentVilla.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Data.Concrete.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(RentVillaDbContext _context) : base(_context)
        {

        }
        private RentVillaDbContext RentVillaDbContext
        {
            get { return _dbContext as RentVillaDbContext; }
        }

        public async Task<List<Product>> GetProductsByAttributeIdAsync(Guid AttributeId)
        {
            List<Product> products = await RentVillaDbContext.ItemAttributes
                .Where(x => x.AttId == AttributeId)
                .Select(x => x.Product)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductWithAttributesAsync(Guid ProductId)
        {
            Product product = await RentVillaDbContext.ItemAttributes
                .Include(x => x.AttributeDesc)
                .ThenInclude(x => x.Name)
                .Where(x => x.ProductId == ProductId)
                .Select(x => x.Product)
                .FirstOrDefaultAsync();
            return product;
        }
    }
}
