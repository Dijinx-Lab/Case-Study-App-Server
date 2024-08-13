using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.Outcomes;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class OutcomeRepository : IOutcomeRepository
    {
        private readonly ApplicationDBContext _context;

        public OutcomeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Outcome> CreateAsync(Outcome outcome)
        {
            await _context.Outcomes.AddAsync(outcome);
            await _context.SaveChangesAsync();
            return outcome;
        }

        public async Task<Outcome?> DeleteAsync(int id)
        {
            var existingItem = await _context.Outcomes.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<Outcome?> UpdateAsync(int id, OutcomeRequestDto requestDto)
        {
            var existingItem = await _context.Outcomes.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.Name = requestDto.Name;
            existingItem.Description = requestDto.Description;
            existingItem.UploadId = requestDto.UploadId;
            existingItem.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<List<Outcome>> GetAllAsync(int? caseStudyId)
        {
            List<Outcome> items;
            if (caseStudyId != null)
            {
                items = await _context.Outcomes
                    .Where(x => x.DeletedOn == null && x.CaseStudyId == caseStudyId)
                    .Include(x => x.Upload)
                    .ToListAsync();
            }
            else
            {
                items = await _context.Outcomes
                    .Where(x => x.DeletedOn == null)
                    .Include(x => x.Upload)
                    .ToListAsync();
            }

            return items.Select(x => x.ToDeleteSafe()).ToList();
        }

        public async Task<Outcome?> GetByIdAsync(int id)
        {
            var item = await _context.Outcomes
                .Include(x => x.Upload)
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);

            return item?.ToDeleteSafe();
        }
    }
}