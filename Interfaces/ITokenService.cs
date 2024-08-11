using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ITokenService
    {
        Task<Token?> CreateToken(AppUser user);
        string GenerateRefreshToken();
    }
}