using MediatR;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Feature.Queries.Products.GetAllProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.ProductImages.GetProductImages
{
    public class GetProductImagesQueryResponse
    {
        public List<ProductImageDTO> ProductImages { get; set; }
    }
}
