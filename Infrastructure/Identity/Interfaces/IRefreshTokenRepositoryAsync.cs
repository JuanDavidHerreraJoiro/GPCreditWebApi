using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Interfaces
{
    public interface IRefreshTokenRepositoryAsync
    {
        Task<RefreshToken> GetChildTokenAsync(string replacedTokenBy);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task<bool> SaveTokenAsync(RefreshToken refreshToken);
        Task<bool> RemoveOldTokensAsync(string userId, int timeToLive);
    }
}
