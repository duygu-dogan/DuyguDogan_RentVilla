using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.CartDTOs;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Repositories.ProductRepo;
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
        private readonly IProductReadRepository _productReadRepository;

        public CartService(IHttpContextAccessor httpContext, UserManager<AppUser> userManager, IReservationReadRepository reservationReadRepository, IResCartItemWriteRepository resCartItemWriteRepository, ILogger<CartService> logger, IResCartWriteRepository resCartWriteRepository, IResCartItemReadRepository resCartItemReadRepository, IMapper mapper, IResCartReadRepository resCartReadRepository, IProductReadRepository productReadRepository)
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
            _productReadRepository = productReadRepository;
        }
        private async Task<ReservationCart> GetTargetCart()
        {
            var username = _httpContext.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users.Include(u => u.Carts)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var userCart = _resCartReadRepository.AppDbContext.Where(rc => rc.UserId == user.Id).Include(rc => rc.CartItems).FirstOrDefault();
                ReservationCart? targetCart = null;
                if (userCart != null)
                {
                    targetCart = userCart;
                }
                else
                {
                    targetCart = new();
                    user.Carts.Add(targetCart);
                    await _resCartWriteRepository.SaveAsync();

                }
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
            try
            {
                ReservationCart targetCart = await GetTargetCart();
                if (targetCart != null)
                {
                    var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.ReservationCartId == targetCart.Id && rci.ProductId == cartItemDTO.ProductId);
                    if (cartItem != null)
                    {
                        ReservationCartItem updatedItem = new ReservationCartItem
                        {
                            Id = cartItem.Id,
                            ProductId = cartItemDTO.ProductId,
                            AdultNumber = cartItemDTO.AdultNumber,
                            ChildrenNumber = cartItemDTO.ChildrenNumber,
                            Note = cartItemDTO.Note,
                            StartDate = cartItemDTO.StartDate,
                            EndDate = cartItemDTO.EndDate,
                            ProductPrice = cartItemDTO.Price,
                            TotalCost = cartItemDTO.TotalCost,
                            ReservationCartId = targetCart.Id,
                            Product = cartItem.Product,
                            ReservationCart = targetCart
                        };
                        _resCartItemWriteRepository.Update(updatedItem);
                    }
                    else
                    {
                        var product = await _productReadRepository.GetByIdAsync(cartItemDTO.ProductId);
                        ReservationCartItem newCartItem = new ReservationCartItem
                        {
                            Id = Guid.NewGuid().ToString(),
                            ProductId = cartItemDTO.ProductId,
                            AdultNumber = cartItemDTO.AdultNumber,
                            ChildrenNumber = cartItemDTO.ChildrenNumber,
                            Note = cartItemDTO.Note,
                            StartDate = cartItemDTO.StartDate,
                            EndDate = cartItemDTO.EndDate,
                            ProductPrice = cartItemDTO.Price,
                            TotalCost = cartItemDTO.TotalCost,
                            ReservationCartId = targetCart.Id,
                            Product = product,
                            ReservationCart = targetCart
                        };

                        await _resCartItemWriteRepository.AddAsync(newCartItem);
                    }
                    await _resCartItemWriteRepository.SaveAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
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
                GetCartItemDTO itemDTO = new();
                itemDTO.AdultNumber = item.AdultNumber;
                itemDTO.ChildrenNumber = item.ChildrenNumber;
                itemDTO.CartItemId = item.Id.ToString();
                itemDTO.Note = item.Note;
                itemDTO.StartDate = item.StartDate;
                itemDTO.EndDate = item.EndDate;
                itemDTO.Product = new ProductDTO
                {
                    Id = item.ProductId.ToString(),
                    Name = item.Product.Name,
                    Description = item.Product.Description,
                    Address = item.Product.Address,
                    Deposit = item.Product.Deposit,
                    MapId = item.Product.MapId,
                    Price = item.Product.Price,
                    ProductAddress = _mapper.Map<ProductAddressDTO>(item.Product.ProductAddress),
                    Properties = item.Product.Properties,
                    ShortestRentPeriod = item.Product.ShortestRentPeriod
                };
                itemDTO.Price = item.ProductPrice;
                itemDTO.TotalCost = item.TotalCost;
                getCartItemDTOs.Add(itemDTO);
            }
            return getCartItemDTOs;
        }

        public async Task RemoveItemFromCartAsync(string cartItemId)
        {
            var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.Id == cartItemId);
            if (cartItem != null)
            {
                _resCartItemWriteRepository.Delete(cartItem);
                await _resCartItemWriteRepository.SaveAsync();
            }
            else
            {
                _logger.LogError("Error in cart service: Cart item not found");
                throw new Exception("Cart item not found");
            }
        }

        public async Task UpdateItemInCartAsync(GetCartItemDTO cartItemDTO)
        {
            var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.Id == cartItemDTO.CartItemId);
            if (cartItem != null)
            {
                cartItem.Note = cartItemDTO.Note;
                cartItem.StartDate = cartItemDTO.StartDate;
                cartItem.EndDate = cartItemDTO.EndDate;
                cartItem.TotalCost = cartItemDTO.TotalCost;
                cartItem.ProductPrice = cartItemDTO.Price;
                cartItem.AdultNumber = cartItemDTO.AdultNumber;
                cartItem.ChildrenNumber = cartItemDTO.ChildrenNumber;
                _resCartItemWriteRepository.Update(cartItem);
                await _resCartItemWriteRepository.SaveAsync();
            }
            else
            {
                _logger.LogError("Error in cart service: Cart item not found");
                throw new Exception("Cart item not found");
            }
        }

        public async Task<GetCartItemDTO> GetCartItemByIdAsync(string cartItemId)
        {
            try
            {
                ReservationCart targetCart = await GetTargetCart();
                var cartItem = await _resCartItemReadRepository.GetSingleAsync(rci => rci.Id == cartItemId);
                if (cartItem != null)
                {
                    return new GetCartItemDTO
                    {
                        AdultNumber = cartItem.AdultNumber,
                        ChildrenNumber = cartItem.ChildrenNumber,
                        CartItemId = cartItem.Id.ToString(),
                        Note = cartItem.Note,
                        StartDate = cartItem.StartDate,
                        EndDate = cartItem.EndDate,
                        Price = cartItem.ProductPrice,
                        TotalCost = cartItem.TotalCost
                    };
                }
                else
                {
                    _logger.LogError("Error in cart service: Cart item not found");
                    throw new Exception("Cart item not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
