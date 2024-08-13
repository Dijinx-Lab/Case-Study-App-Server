using CaseStudyAppServer.Dtos.Challenge;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IChallengeRepository
    {
        Task<List<Challenge>> GetByCaseStudyIdAsync(int id);
        Task<Challenge?> GetByIdAsync(int id);
        Task<Challenge> CreateAsync(Challenge challenge);
        Task<Challenge?> UpdateAsync(int id, ChallengeRequestDto challengeRequestDto);
        Task<Challenge?> DeleteAsync(int id);
    }
}