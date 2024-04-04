using RentVilla.Application.DTOs.TokenDTOs;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken);
    }
}
