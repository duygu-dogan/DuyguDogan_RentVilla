using RentVilla.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetDeletedProducts
{
    public class GetDeletedProductsResponse
    {
        public List<ProductDTO> DeletedProducts { get; set; }
    }
}
