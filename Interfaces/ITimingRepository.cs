

using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ITimingRepository
    {
        Task<Timing> CreateAsync(int teamId, int caseStudyId, DateTime time);
        Task<Timing?> UpdateAsync(int teamId, int caseStudyId, DateTime time);
        Task<List<Timing>> GetAllByTeamIdAsync(int id);
        Task<List<Timing>> GetAllByCaseStudyIdAsync(int id);
    }
}