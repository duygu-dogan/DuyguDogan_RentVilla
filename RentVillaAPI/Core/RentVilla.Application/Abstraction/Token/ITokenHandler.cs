using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(AppUser user, int minute);
    }
}
