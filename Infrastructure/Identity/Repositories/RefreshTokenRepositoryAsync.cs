using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Repositories
{
    public class RefreshTokenRepositoryAsync : IRefreshTokenRepositoryAsync
    {
        public readonly IdentityContext _context;
        public RefreshTokenRepositoryAsync(IdentityContext context) 
        {
            _context = context;
        }

        public async Task<RefreshToken> GetChildTokenAsync(string replacedTokenBy)
        {
            var childToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == replacedTokenBy);

            return childToken!;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            var childToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);

            return childToken!;
        }

        public async Task<bool> RemoveOldTokensAsync(string userId, int timeToLive)
        {
            var tokens = await _context.RefreshTokens.Where(x => x.ApplicationUserId == userId).ToListAsync();

            var oldTokens = tokens.Where(x => !x.IsActive &&
                x.Created.AddDays(timeToLive) <= DateTime.UtcNow).ToList();

            _context.RefreshTokens!.RemoveRange(oldTokens);

            var removed = _context.SaveChangesAsync();

            return removed.Result > 0;
        }

        public async Task<bool> SaveTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);

            var saved = _context.SaveChangesAsync();

            return saved.Result > 0;
        }
    }
}
