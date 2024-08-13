using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.CaseStudy;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ICaseStudyRepository
    {
        Task<List<CaseStudy>> GetAllAsync();
        Task<CaseStudy?> GetByIdAsync(int id);
        Task<CaseStudy> CreateAsync(CaseStudy caseStudy);
        Task<CaseStudy?> UpdateAsync(int id, CaseStudyRequestDto caseStudyRequestDto);
        Task<CaseStudy?> DeleteAsync(int id);
    }
}