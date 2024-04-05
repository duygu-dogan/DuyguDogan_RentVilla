using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.AuthDTOs
{
    public class LoginResponseDTO
    {
        public LoginUserResponseDTO UserData { get; set; }
        public TokenDTO Token { get; set; }
    }
}
