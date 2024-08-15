using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.LeadershipStrategy;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class LeadershipStrategyRepository : ILeadershipStrategyRepository
    {
        private readonly ApplicationDBContext _context;

        public LeadershipStrategyRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<LeadershipStrategy> CreateAsync(LeadershipStrategy item)
        {
            await _context.LeadershipStrategies.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<LeadershipStrategy?> DeleteAsync(int id)
        {
            var existingItem = await _context.LeadershipStrategies.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<LeadershipStrategy?> UpdateAsync(int id, LeadershipStrategyRequestDto requestDto)
        {
            var existingItem = await _context.LeadershipStrategies.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.Name = requestDto.Name;
            existingItem.Description = requestDto.Description;
            existingItem.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<List<LeadershipStrategy>> GetAllAsync(int? caseStudyId)
        {
            List<LeadershipStrategy> items;
            if (caseStudyId != null)
            {
                items = await _context.LeadershipStrategies
                    .Where(x => x.DeletedOn == null && x.CaseStudyId == caseStudyId)
                    .ToListAsync();
            }
            else
            {
                items = await _context.LeadershipStrategies
                    .Where(x => x.DeletedOn == null)
                    .ToListAsync();
            }
            return items;
        }

        public async Task<LeadershipStrategy?> GetByIdAsync(int id)
        {
            var item = await _context.LeadershipStrategies
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            return item;
        }

        public async Task DeleteByCaseStudyIdAsync(int id)
        {
            var items = await _context.LeadershipStrategies
            .Where(x => x.CaseStudyId == id && x.DeletedOn == null)
            .ToListAsync();

            foreach (var item in items)
            {
                item.DeletedOn = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}