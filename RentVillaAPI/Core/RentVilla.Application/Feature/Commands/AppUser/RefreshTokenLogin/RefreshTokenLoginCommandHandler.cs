using MediatR;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDTO token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new RefreshTokenLoginCommandResponse() { Token = token };
        }
    }
}
