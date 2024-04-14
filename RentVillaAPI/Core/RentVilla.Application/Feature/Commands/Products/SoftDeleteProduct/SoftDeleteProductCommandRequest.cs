using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Products.SoftDeleteProduct
{
    public class SoftDeleteProductCommandRequest : IRequest<SoftDeleteProductCommandResponse>
    {
        public string ProductId { get; set; }
    }
}
