using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ITokenRepository
    {
        Task<Token> CreateAndDeletePreviousAsync(string appUserId, string token, string refreshToken, DateTime expires);
        Task<bool?> ValidateAsync(string token); //NULL MEANS EXISTS BUT EXPIRED
        Task<Token?> ValidateRefreshTokenAsync(string token, string refreshToken);
        Task<Token?> GetByRefreshTokenAsync(string refreshToken); //NULL MEANS EXISTS BUT EXPIRED
        Task DeleteTokensByUserIdAsync(string appUserId);
    }
}