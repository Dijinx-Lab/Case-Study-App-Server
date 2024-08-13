using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.LeadershipStrategy;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ILeadershipStrategyRepository
    {
        Task<List<LeadershipStrategy>> GetAllAsync(int? caseStudyId);
        Task<LeadershipStrategy?> GetByIdAsync(int id);
        Task<LeadershipStrategy> CreateAsync(LeadershipStrategy strategy);
        Task<LeadershipStrategy?> UpdateAsync(int id, LeadershipStrategyRequestDto requestDto);
        Task<LeadershipStrategy?> DeleteAsync(int id);
    }
}