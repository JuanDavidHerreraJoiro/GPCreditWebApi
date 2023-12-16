using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(ApplicationUser user, string role);
        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
