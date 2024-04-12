using RentVilla.Application.DTOs.CartDTOs;
using RentVilla.Domain.Entities.Concrete.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Services
{
    public interface ICartService
    {
        public Task<List<GetCartItemDTO>> GetCartItemsAsync();
        public Task<GetCartItemDTO> GetCartItemByIdAsync(string cartItemId);
        public Task AddItemToCartAsync(AddCartItemDTO cartItemDTO);
        public Task RemoveItemFromCartAsync(string cartItemId);
        public Task UpdateItemInCartAsync(GetCartItemDTO cartItemDTO);
    }
}
