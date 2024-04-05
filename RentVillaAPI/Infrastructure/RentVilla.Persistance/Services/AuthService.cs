using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.Abstraction.Token;
using RentVilla.Application.DTOs.AuthDTOs;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Exceptions;
using RentVilla.Domain.Entities.Concrete.Identity;

namespace RentVilla.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<TokenDTO> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                TokenDTO token = _tokenHandler.CreateAccessToken(user, accessTokenLifeTime);
                
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 30);
                return token;
            }
            else
                throw new NotFoundUserException("Username/email or password is wrong!");
        }

        public async Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
               TokenDTO token = _tokenHandler.CreateAccessToken(user, 30);
               await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 30);
                return token;
            }
            throw new NotFoundUserException();

        }
    }
    
}
