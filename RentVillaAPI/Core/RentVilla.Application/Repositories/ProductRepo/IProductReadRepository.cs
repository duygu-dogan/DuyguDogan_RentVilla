using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Repositories;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories.ProductRepo
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
        //Fiyat aralığına göre ürünleri getirir.
        Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        //Ürün bölgesine göre ürünleri getirir.
        Task<IEnumerable<ProductDTO>> GetProductsByRegion(string regionId);
        //En kısa süreliğine kiralanabilir ürünleri getirir.
        Task<IEnumerable<Product>> GetProductsByShortestRentTime();
        //ProductAddress ve ProductAttribute tablolarını join ederek ürünleri getirir.
        Task<ProductDTO> GetJoinedProductByIdAsync(string id);
        ICollection<ProductDTO> GetAllProducts();

    }
}
