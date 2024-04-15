using AutoMapper;
using RentVilla.Application.DTOs.CartDTOs;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.DTOs.ReservationDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Feature.Commands.AppUser.CreateUser;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete.Region;

namespace RentVilla.Persistence.Mappings
{
    public class GeneralMappingProfile: Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<CreateUserCommandRequest, CreateUserDTO>().ReverseMap();

            CreateMap<UserAddress, UserAddressDTO>().ReverseMap();
            CreateMap<AppUser, CreateUserDTO>()
                .ForMember(dto => dto.UserAddress, options=>
                options.MapFrom(au => au.UserAddress)).ReverseMap();
            CreateMap<AppUser, LoginUserResponseDTO>()
                .ForMember(dto => dto.UserAddress, options =>
                options.MapFrom(au => au.UserAddress)).ReverseMap();
            CreateMap<ReservationCartItem, AddCartItemDTO>().ReverseMap();
            CreateMap<ReservationCartItem, GetCartItemDTO>();
            CreateMap<Reservation, CreateReservationDTO>();
            CreateMap<Reservation, GetReservationDTO>();
            CreateMap<Domain.Entities.Concrete.File, ProductImageDTO>();
            CreateMap<ProductAddress, ProductAddressDTO>();
        }
    }
}
