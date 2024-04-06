using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Domain.Entities.Concrete.Identity;
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
        private readonly UserManager<Domain.Entities.Concrete.Identity.AppUser> _userManager;
        private readonly IMapper _mapper;

        public RefreshTokenLoginCommandHandler(IAuthService authService, UserManager<Domain.Entities.Concrete.Identity.AppUser> userManager, IMapper mapper)
        {
            _authService = authService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDTO token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            Domain.Entities.Concrete.Identity.AppUser user = await _userManager.Users.FirstOrDefaultAsync(rt => rt.RefreshToken == token.RefreshToken);
            LoginUserResponseDTO userData = _mapper.Map<LoginUserResponseDTO>(user);
            
            return new RefreshTokenLoginCommandResponse() { Token = token, UserData = userData };
        }
    }
}
