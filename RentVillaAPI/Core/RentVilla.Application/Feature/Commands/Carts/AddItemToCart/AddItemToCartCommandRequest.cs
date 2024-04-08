using MediatR;
using RentVilla.Application.DTOs.CartDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandRequest: IRequest<AddItemToCartCommandResponse>
    {
        public AddCartItemDTO cartItemDTO { get; set; }
    }
}
