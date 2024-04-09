using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.CartDTOs;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Repositories.ReservationCartItemRepo;
using RentVilla.Application.Repositories.ReservationCartRepo;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Persistance.Contexts;

namespace RentVilla.Persistence.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IResCartWriteRepository _resCartWriteRepository;
        private readonly IResCartReadRepository _resCartReadRepository;
        private readonly ILogger<CartService> _logger;
        private readonly IResCartItemWriteRepository _resCartItemWriteRepository;
        private readonly IResCartItemReadRepository _resCartItemReadRepository;
        private readonly IMapper _mapper;

        public CartService(IHttpContextAccessor httpContext, UserManager<AppUser> userManager, IReservationReadRepository reservationReadRepository, IResCartItemWriteRepository resCartItemWriteRepository, ILogger<CartService> logger, IResCartWriteRepository resCartWriteRepository, IResCartItemReadRepository resCartItemReadRepository, IMapper mapper, IResCartReadRepository resCartReadRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContext = httpContext;
            _userManager = userManager;
            _reservationReadRepository = reservationReadRepository;
            _resCartItemWriteRepository = resCartItemWriteRepository;
            _resCartWriteRepository = resCartWriteRepository;
            _resCartItemReadRepository = resCartItemReadRepository;
            _resCartReadRepository = resCartReadRepository;
        }
        private async Task<ReservationCart> GetTargetCart()
        {
            var username = _httpContext.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users.Include(u => u.Carts)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var userCart = from cart in user.Carts
                               join reservation in _reservationReadRepository.AppDbContext
                               on cart.Id equals reservation.Id into CartReservations
                               from cr in CartReservations.DefaultIfEmpty()
                               select new
                               {
                                   Cart = cart,
                                  Reservation = cr
                               };
                ReservationCart? targetCart = null;
                if (userCart.Any(r => r.Reservation is null))
                {
                    targetCart =userCart.First(r => r.Reservation is null)?.Cart;
                }
                else
                {
                    targetCart = new();
                    user.Carts.Add(targetCart);
                }
                await _resCartWriteRepository.SaveAsync();
                return targetCart;
            }
            else
            {
                _logger.LogError("Error in cart service: User not found");
                throw new NotFoundUserException("User not found");
            }
        }

        public async Task AddItemToCartAsync(AddCartItemDTO cartItemDTO)
        {
             ReservationCart targetCart = await GetTargetCart();
            if(targetCart != null)
            {
               var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.Id == targetCart.Id && rci.ProductId == Guid.Parse(cartItemDTO.ProductId) && rci.StartDate == cartItemDTO.StartDate && rci.EndDate == cartItemDTO.EndDate);
                if(cartItem != null)
                {
                    ReservationCartItem updatedItem = _mapper.Map<ReservationCartItem>(cartItemDTO);
                    _resCartItemWriteRepository.Update(updatedItem);
                }
                else
                {
                    ReservationCartItem newCartItem = new ReservationCartItem();
                    newCartItem = _mapper.Map<ReservationCartItem>(cartItemDTO);
                    newCartItem.ReservationCartId = targetCart.Id;
                    await _resCartItemWriteRepository.AddAsync(newCartItem);
                }
                await _resCartItemWriteRepository.SaveAsync();
            }
           
        }

        public async Task<List<GetCartItemDTO>> GetCartItemsAsync()
        {
            ReservationCart reservationCart = await GetTargetCart();
            var result = await _resCartReadRepository.GetAll()
                .Include(rc => rc.CartItems)
                .ThenInclude(rci => rci.Product)
                .FirstOrDefaultAsync(c => c.Id == reservationCart.Id);
            List<GetCartItemDTO> getCartItemDTOs = new();
            foreach (var item in result.CartItems)
            {
                GetCartItemDTO getCartItemDTO =  _mapper.Map<GetCartItemDTO>(item);
                getCartItemDTOs.Add(getCartItemDTO);
            }
            return getCartItemDTOs;
        }

        public async Task RemoveItemFromCartAsync(string cartItemId)
        {
            var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.Id == Guid.Parse(cartItemId));
            if(cartItem != null)
            {
                _resCartItemWriteRepository.Delete(cartItem);
               await _resCartItemWriteRepository.SaveAsync();
            }else
            {
                _logger.LogError("Error in cart service: Cart item not found");
                throw new Exception("Cart item not found");
            }
        }

        public async Task UpdateItemInCartAsync(UpdateCartItemDTO cartItemDTO)
        {
            var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.Id == Guid.Parse(cartItemDTO.CartItemId));
            if(cartItem != null)
            {
                ReservationCartItem updatedItem = _mapper.Map<ReservationCartItem>(cartItemDTO);
                _resCartItemWriteRepository.Update(updatedItem);
                await _resCartItemWriteRepository.SaveAsync();
            }
            else
            {
                _logger.LogError("Error in cart service: Cart item not found");
                throw new Exception("Cart item not found");
            }
        }
    }
}
