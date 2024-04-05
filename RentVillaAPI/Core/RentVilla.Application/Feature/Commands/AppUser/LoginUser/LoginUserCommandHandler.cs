using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.AuthDTOs;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Exceptions;

namespace RentVilla.Application.Feature.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly UserManager<Domain.Entities.Concrete.Identity.AppUser> _userManager;
        readonly IMapper _mapper;

        public LoginUserCommandHandler(IAuthService authService, IMapper mapper, UserManager<Domain.Entities.Concrete.Identity.AppUser> userManager)
        {
            _authService = authService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Concrete.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            LoginUserResponseDTO loginUser = _mapper.Map<LoginUserResponseDTO>(user);
            
            TokenDTO token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 30);
            return new LoginUserSuccessCommandResponse
            {
                Token = token,
                UserData = loginUser
            };
        }
    }
}
