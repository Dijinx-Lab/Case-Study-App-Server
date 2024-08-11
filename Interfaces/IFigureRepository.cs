using CaseStudyAppServer.Dtos.Figure;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IFigureRepository
    {
        Task<List<Figure>> GetAllAsync();
        Task<Figure?> GetByIdAsync(int id);
        Task<Figure> CreateAsync(Figure figure);
        Task<Figure?> UpdateAsync(int id, FigureRequestDto figureRequestDto);
        Task<Figure?> DeleteAsync(int id);
    }
}