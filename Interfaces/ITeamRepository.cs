using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllAsync();
        Task<Team?> GetByCodeAsync(string teamCode);
        Task<Team> CreateAsync(string name);
        Task<Team?> UpdateAsync(string code, string name);
        Task<Team?> DeleteAsync(string teamCode);
    }
}