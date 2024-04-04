using RentVilla.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandResponse
    {
        public TokenDTO Token { get; set; }
    }
}
