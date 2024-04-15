using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Feature.Commands.AppUser.CreateUser;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete.Region;

namespace RentVilla.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IStateReadRepository _stateReadRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IStateReadRepository stateReadRepository, ICityReadRepository cityReadRepository, IDistrictReadRepository districtReadRepository, ICountryReadRepository countryReadRepository, IMapper mapper, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _stateReadRepository = stateReadRepository;
            _cityReadRepository = cityReadRepository;
            _districtReadRepository = districtReadRepository;
            _countryReadRepository = countryReadRepository;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToUserAsync(string userId, List<string> roleIds)
        {
            try
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);

                    foreach (var roleId in roleIds)
                    {
                        var role = await _roleManager.FindByIdAsync(roleId);
                        var roleName = await _roleManager.GetRoleNameAsync(role);

                        await _userManager.AddToRoleAsync(user, roleName);
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
        {

            try
            {
                AppUser newUser = _mapper.Map<AppUser>(model);
                newUser.Id = Guid.NewGuid().ToString(); 
                newUser.UserAddress = new UserAddress()
                {
                    State = await _stateReadRepository.GetByIdAsync(model.UserAddress.StateId),
                    City = await _cityReadRepository.GetByIdAsync(model.UserAddress.CityId),
                    District = await _districtReadRepository.GetByIdAsync(model.UserAddress.DistrictId)
                };
                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
                CreateUserResponseDTO response = new CreateUserResponseDTO() { Succeeded = result.Succeeded };
                if (result.Succeeded)
                {
                    response.Message = "User added successfully";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.Message += $"{error.Code}-{error.Description} <br>";
                    }
                }
                return response;
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users.Select(user => new GetUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Address = user.Address
            }).ToList();
        }

        public async Task<List<AppRole>> GetUserRoles(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roleIds = await _userManager.GetRolesAsync(user);
                List<AppRole> roles = new();
                foreach (var roleId in roleIds)
                {
                    var role = await _roleManager.FindByNameAsync(roleId);
                    roles.Add(role);
                }
                return roles;
            }
            else
            {
                throw new NotFoundUserException();
            }
        }

        public async Task<TokenDTO> UpdateRefreshToken(TokenDTO token, string refreshToken, AppUser user, DateTime accessTokenDate, int addOnrefreshTokenEnd)
        {
            try
            {
                if (user != null)
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnrefreshTokenEnd).ToUniversalTime();
                    await _userManager.UpdateAsync(user);
                    token.RefreshToken = refreshToken;
                    token.RefreshTokenEndDate = user.RefreshTokenEndDate;
                    return token;
                }
                else
                {
                    throw new NotFoundUserException();
                }
            }
            catch (Exception)
            {
                throw new NotFoundUserException();
            }

        }

    }
}
