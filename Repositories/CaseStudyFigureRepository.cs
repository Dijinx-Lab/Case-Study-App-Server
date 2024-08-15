using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.CaseStudyFigure;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class CaseStudyFigureRepository : ICaseStudyFigureRepository
    {
        private readonly ApplicationDBContext _context;
        public CaseStudyFigureRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CaseStudyFigure> CreateAsync(CaseStudyFigure caseStudyFigure)
        {
            await _context.CaseStudyFigures.AddAsync(caseStudyFigure);
            await _context.SaveChangesAsync();
            return caseStudyFigure;
        }

        public async Task<CaseStudyFigure?> DeleteAsync(int caseStudyId, int figureId)
        {
            var existingItem = await _context.CaseStudyFigures.FirstOrDefaultAsync(x => x.CaseStudyId == caseStudyId && x.FigureId == figureId);
            if (existingItem == null) return null;

            _context.CaseStudyFigures.Remove(existingItem);

            await _context.SaveChangesAsync();

            return existingItem;
        }

        public async Task<List<Figure>> GetByCaseStudyIdAsync(int caseStudyId)
        {
            var items = await _context.CaseStudyFigures
                .Where(x => x.CaseStudyId == caseStudyId && x.Figure.DeletedOn == null)
                .Select(x => x.Figure)
                .ToListAsync();

            return items;
        }


        public async Task<List<CaseStudy>> GetByFigureIdAsync(int figureId)
        {
            var items = await _context.CaseStudyFigures
            .Where(x => x.FigureId == figureId && x.CaseStudy.DeletedOn == null)
            .Select(c => c.CaseStudy)
            .ToListAsync();

            return items;
        }
    }
}