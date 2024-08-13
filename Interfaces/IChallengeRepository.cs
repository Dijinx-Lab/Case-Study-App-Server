using CaseStudyAppServer.Dtos.Challenges;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IChallengeRepository
    {
        Task<List<Challenge>> GetAllAsync(int? caseStudyId);
        Task<Challenge?> GetByIdAsync(int id);
        Task<Challenge> CreateAsync(Challenge challenge);
        Task<Challenge?> UpdateAsync(int id, ChallengeRequestDto challengeRequestDto);
        Task<Challenge?> DeleteAsync(int id);
    }
}