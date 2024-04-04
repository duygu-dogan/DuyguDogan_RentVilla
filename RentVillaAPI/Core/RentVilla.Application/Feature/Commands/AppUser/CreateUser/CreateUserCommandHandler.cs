using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Repositories.RegionRepo;

namespace RentVilla.Application.Feature.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var createUserDTO = _mapper.Map<CreateUserDTO>(request);
            var response = await _userService.CreateAsync(createUserDTO);
            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
        }
    }
}
