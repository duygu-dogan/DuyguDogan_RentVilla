using RentVilla.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetFilteredProducts
{
    public class GetFilteredProductsQueryResponse
    {
        public List<ProductDTO> FilteredProducts { get; set; }
    }
}
