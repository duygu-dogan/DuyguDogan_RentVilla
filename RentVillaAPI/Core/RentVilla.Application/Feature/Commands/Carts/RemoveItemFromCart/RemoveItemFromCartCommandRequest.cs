using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Carts.RemoveItemFromCart
{
    public class RemoveItemFromCartCommandRequest : IRequest<RemoveItemFromCartCommandResponse>
    {
        public string CartItemId { get; set; }

    }
}
