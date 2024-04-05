﻿using RentVilla.Application.DTOs.AuthDTOs;
using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;

namespace RentVilla.Application.Feature.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
    }
    public class LoginUserSuccessCommandResponse: LoginUserCommandResponse
    {
        public TokenDTO Token { get; set; }
        public LoginUserResponseDTO UserData { get; set; }
    }
    public class LoginUserFailCommandResponse: LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}