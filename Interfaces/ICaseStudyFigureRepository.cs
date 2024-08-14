using CaseStudyAppServer.Dtos.CaseStudyFigure;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface ICaseStudyFigureRepository
    {
        Task<List<Figure>> GetByCaseStudyIdAsync(int caseStudyId);
        Task<List<CaseStudy>> GetByFigureIdAsync(int figureId);
        Task<CaseStudyFigure> CreateAsync(CaseStudyFigure caseStudyFigure);
        Task<CaseStudyFigure?> DeleteAsync(int caseStudyId, int figureId);
    }
}