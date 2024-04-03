using RentVilla.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetAllProducts
{
    public class GetAllProductsQueryResponse
    {
        public List<ProductDTO> NonDeletedProducts { get; set; }
    }
}
