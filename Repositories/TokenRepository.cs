using CaseStudyAppServer.Data;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDBContext _context;

        public TokenRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool?> ValidateAsync(string token)
        {
            var tokenModel = await _context.Tokens.FirstOrDefaultAsync(x => x.Value == token);

            if (tokenModel == null) return false;

            if (tokenModel.ExpiresOn < DateTime.UtcNow) return null;

            return true;
        }
        public async Task<Token> CreateAndDeletePreviousAsync(string appUserId, string token, string refreshToken, DateTime expires)
        {
            await DeleteTokensByUserIdAsync(appUserId);
            var dbToken = new Token
            {
                Value = token,
                RefreshValue = refreshToken,
                ExpiresOn = expires,
                AppUserId = appUserId,
            };
            await _context.Tokens.AddAsync(dbToken);
            await _context.SaveChangesAsync();
            return dbToken;
        }

        public async Task DeleteTokensByUserIdAsync(string appUserId)
        {
            var tokenModels = _context.Tokens.Where(x => x.AppUserId == appUserId).ToList();
            if (tokenModels.Count == 0) return;
            _context.Tokens.RemoveRange(tokenModels);

            await _context.SaveChangesAsync();

        }

        public async Task<Token?> GetByRefreshTokenAsync(string refreshToken)
        {
            var existingRefreshToken = await _context.Tokens
            .FirstOrDefaultAsync(x => x.RefreshValue == refreshToken);
            return existingRefreshToken;
        }

        public async Task<Token?> ValidateRefreshTokenAsync(string token, string refreshToken)
        {
            var tokenModel = await _context.Tokens.FirstOrDefaultAsync(x => x.Value == token && x.RefreshValue == refreshToken);

            if (tokenModel == null) return null;

            return tokenModel;
        }
    }
}