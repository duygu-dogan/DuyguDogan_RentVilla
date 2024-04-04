using AutoMapper;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Feature.Commands.AppUser.CreateUser;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
