using MediatR;
using Microsoft.AspNetCore.Identity;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.Abstraction.Token;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.Exceptions;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 30);
            return new LoginUserSuccessCommandResponse
            {
                Token = token
            };
        }
    }
}
