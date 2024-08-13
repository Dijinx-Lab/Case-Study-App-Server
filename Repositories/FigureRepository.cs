using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.Figures;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class FigureRepository : IFigureRepository
    {
        private readonly ApplicationDBContext _context;

        public FigureRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Figure> CreateAsync(Figure figure)
        {
            await _context.Figures.AddAsync(figure);
            await _context.SaveChangesAsync();
            return figure;
        }

        public async Task<Figure?> DeleteAsync(int id)
        {
            var existingFigure = await _context.Figures.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingFigure == null) return null;

            existingFigure.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingFigure;
        }

        public async Task<Figure?> UpdateAsync(int id, FigureRequestDto figureRequestDto)
        {
            var existingFigure = await _context.Figures.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingFigure == null) return null;

            existingFigure.Name = figureRequestDto.Name;
            existingFigure.Description = figureRequestDto.Description;
            existingFigure.UploadId = figureRequestDto.UploadId;
            existingFigure.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingFigure;
        }

        public async Task<List<Figure>> GetAllAsync()
        {
            var uploads = await _context.Figures
            .Where(x => x.DeletedOn == null)
            .Include(x => x.Upload)
            .ToListAsync();
            return uploads.Select(x => x.ToDeleteSafe()).ToList();
        }

        public async Task<Figure?> GetByIdAsync(int id)
        {
            var existingReview = await _context.Figures
            .Include(x => x.Upload)
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            return existingReview?.ToDeleteSafe();
        }
    }
}