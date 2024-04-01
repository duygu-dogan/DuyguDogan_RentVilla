using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandRequest: IRequest<DeleteProductCommandResponse>
    {
        public string ProductId { get; set; }
    }
}
