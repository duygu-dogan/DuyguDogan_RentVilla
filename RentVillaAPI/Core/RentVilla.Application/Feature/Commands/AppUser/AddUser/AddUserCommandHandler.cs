using MediatR;
using Microsoft.AspNetCore.Identity;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Repositories.RegionRepo;

namespace RentVilla.Application.Feature.Commands.AppUser.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommandRequest, AddUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Concrete.Identity.AppUser> _userManager;
        private readonly IStateReadRepository _stateReadRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly ICountryReadRepository _countryReadRepository;

        public AddUserCommandHandler(UserManager<Domain.Entities.Concrete.Identity.AppUser> userManager, IStateReadRepository stateReadRepository, ICityReadRepository cityReadRepository, IDistrictReadRepository districtReadRepository, ICountryReadRepository countryReadRepository)
        {
            _userManager = userManager;
            _stateReadRepository = stateReadRepository;
            _cityReadRepository = cityReadRepository;
            _districtReadRepository = districtReadRepository;
            _countryReadRepository = countryReadRepository;
        }

        public async Task<AddUserCommandResponse> Handle(AddUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IdentityResult result = await _userManager.CreateAsync(new Domain.Entities.Concrete.Identity.AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    UserAddress = new Domain.Entities.Concrete.Region.UserAddress()
                     {
                        State = await _stateReadRepository.GetByIdAsync(request.UserAddress.StateId),
                        City = await _cityReadRepository.GetByIdAsync(request.UserAddress.CityId),
                        District = await _districtReadRepository.GetByIdAsync(request.UserAddress.DistrictId)
                     }  
                }, request.Password);
                AddUserCommandResponse response = new AddUserCommandResponse() { Succeeded = result.Succeeded };
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
    }
}
