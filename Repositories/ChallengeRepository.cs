using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.Challenges;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly ApplicationDBContext _context;

        public ChallengeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Challenge> CreateAsync(Challenge challenge)
        {
            await _context.Challenges.AddAsync(challenge);
            await _context.SaveChangesAsync();
            return challenge;
        }

        public async Task<Challenge?> DeleteAsync(int id)
        {
            var existingItem = await _context.Challenges.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<Challenge?> UpdateAsync(int id, ChallengeRequestDto challengeRequestDto)
        {
            var existingItem = await _context.Challenges.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.Name = challengeRequestDto.Name;
            existingItem.Description = challengeRequestDto.Description;
            existingItem.UploadId = challengeRequestDto.UploadId;
            existingItem.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<List<Challenge>> GetAllAsync(int? caseStudyId)
        {
            List<Challenge> items = [];
            if (caseStudyId != null)
            {
                items = await _context.Challenges
                .Where(x => x.DeletedOn == null && x.CaseStudyId == caseStudyId)
                .Include(x => x.Upload)
                .ToListAsync();
            }
            else
            {
                items = await _context.Challenges
                .Where(x => x.DeletedOn == null)
                .Include(x => x.Upload)
                .ToListAsync();
            }

            return items;
        }

        public async Task<Challenge?> GetByIdAsync(int id)
        {
            var item = await _context.Challenges
            .Include(x => x.Upload)
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            return item;
        }
    }
}