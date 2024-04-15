using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);
        Task<TokenDTO> UpdateRefreshToken(TokenDTO token, string refreshToken, AppUser user, DateTime accessTokenDate, int addOnrefreshTokenEnd);
        Task<List<GetUserDTO>> GetAllUsersAsync(int page, int size);

    }
}
