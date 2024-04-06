using RentVilla.Application.DTOs.TokenDTOs;
using RentVilla.Domain.Entities.Concrete.Identity;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(AppUser user, string password, int accessTokenLifeTime);
        Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken);
    }
}
