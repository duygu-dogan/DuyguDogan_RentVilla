using MediatR;
using RentVilla.Application.DTOs.CartDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Carts.UpdateItemInCart
{
    public class UpdateItemInCartCommandRequest: IRequest<UpdateItemInCartCommandResponse>
    {
        public GetCartItemDTO cartItemDTO { get; set; }
    }
}
