using CaseStudyAppServer.Data;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class TimingRepository : ITimingRepository
    {
        private readonly ApplicationDBContext _context;

        public TimingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Timing> CreateAsync(int teamId, int caseStudyId, DateTime time)
        {
            var timing = new Timing
            {
                TeamId = teamId,
                CaseStudyId = caseStudyId,
                StartedOn = time,
            };
            await _context.Timings.AddAsync(timing);
            await _context.SaveChangesAsync();
            return timing;
        }

        public async Task<Timing?> UpdateAsync(int teamId, int caseStudyId, DateTime time)
        {
            var existingItem = await _context.Timings
            .FirstOrDefaultAsync(x => x.TeamId == teamId && x.CaseStudyId == caseStudyId && x.DeletedOn == null);
            if (existingItem == null) return null;

            if (time <= existingItem.StartedOn) return null;

            existingItem.EndedOn = time;
            existingItem.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }


        public async Task<List<Timing>> GetAllByCaseStudyIdAsync(int id)
        {
            List<Timing> items = await _context.Timings
                    .Where(x => x.DeletedOn == null && x.CaseStudyId == id)
                    .ToListAsync();

            return items;
        }

        public async Task<List<Timing>> GetAllByTeamIdAsync(int id)
        {
            List<Timing> items = await _context.Timings
                   .Where(x => x.DeletedOn == null && x.TeamId == id)
                   .ToListAsync();

            return items;
        }

    }
}