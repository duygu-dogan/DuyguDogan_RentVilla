using MediatR;
using Microsoft.AspNetCore.Identity;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.Abstraction.Token;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Feature.Commands.AppUser.LoginUser;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
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
                return token;
            }
            else
                throw new NotFoundUserException("Username/email or password is wrong!");
        }
    }
    
}
