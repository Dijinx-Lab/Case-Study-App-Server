using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Outcomes;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IOutcomeRepository
    {
        Task<List<Outcome>> GetAllAsync(int? caseStudyId);
        Task<Outcome?> GetByIdAsync(int id);
        Task<Outcome> CreateAsync(Outcome outcome);
        Task<Outcome?> UpdateAsync(int id, OutcomeRequestDto requestDto);
        Task<Outcome?> DeleteAsync(int id);
    }
}