using RentVilla.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
    }
    public class LoginUserSuccessCommandResponse: LoginUserCommandResponse
    {
        public TokenDTO Token { get; set; }
    }
    public class LoginUserFailCommandResponse: LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
