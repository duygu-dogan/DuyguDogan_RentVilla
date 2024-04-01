using MediatR;
using RentVilla.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandRequest: IRequest<UpdateProductCommandResponse>
    {
        public ProductDTO Product { get; set; }
    }
}
