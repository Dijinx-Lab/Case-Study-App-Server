using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.CaseStudy;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class CaseStudyRepository : ICaseStudyRepository
    {
        private readonly ApplicationDBContext _context;

        public CaseStudyRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CaseStudy> CreateAsync(CaseStudy caseStudy)
        {
            await _context.CaseStudies.AddAsync(caseStudy);
            await _context.SaveChangesAsync();
            return caseStudy;
        }

        public async Task<CaseStudy?> DeleteAsync(int id)
        {
            var existingItem = await _context.CaseStudies.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.DeletedOn = DateTime.UtcNow;
            //TODO: DELETE ALL THE RELATED OBJS
            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<CaseStudy?> UpdateAsync(int id, CaseStudyRequestDto caseStudyRequestDto)
        {
            var existingItem = await _context.CaseStudies.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.Name = caseStudyRequestDto.Name;
            existingItem.Description = caseStudyRequestDto.Description;
            existingItem.CoverUploadId = caseStudyRequestDto.CoverUploadId;

            if (caseStudyRequestDto.OverviewDescription != null)
                existingItem.OverviewDescription = caseStudyRequestDto.OverviewDescription;
            if (caseStudyRequestDto.BackgroundDescription != null)
                existingItem.BackgroundDescription = caseStudyRequestDto.BackgroundDescription;
            if (caseStudyRequestDto.SituationDescription != null)
                existingItem.SituationDescription = caseStudyRequestDto.SituationDescription;
            if (caseStudyRequestDto.ConclusionDescription != null)
                existingItem.ConclusionDescription = caseStudyRequestDto.ConclusionDescription;

            if (caseStudyRequestDto.OverviewUploadId != null)
                existingItem.OverviewUploadId = caseStudyRequestDto.OverviewUploadId;
            if (caseStudyRequestDto.BackgroundUploadId != null)
                existingItem.BackgroundUploadId = caseStudyRequestDto.BackgroundUploadId;
            if (caseStudyRequestDto.SituationUploadId != null)
                existingItem.SituationUploadId = caseStudyRequestDto.SituationUploadId;
            if (caseStudyRequestDto.ConclusionUploadId != null)
                existingItem.ConclusionUploadId = caseStudyRequestDto.ConclusionUploadId;

            existingItem.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<List<CaseStudy>> GetAllAsync()
        {
            var uploads = await _context.CaseStudies
           .Where(x => x.DeletedOn == null)
           .Include(x => x.CoverUpload)
           .Include(x => x.OverviewUpload)
           .Include(x => x.BackgroundUpload)
           .Include(x => x.SituationUpload)
           .Include(x => x.ConclusionUpload)
           //TODO: INCLUDE AND THEN INCLUDE REST OF THE OBJS
           .ToListAsync();
            return uploads;
        }

        public async Task<CaseStudy?> GetByIdAsync(int id)
        {
            var upload = await _context.CaseStudies
            .Include(x => x.CoverUpload)
            .Include(x => x.OverviewUpload)
            .Include(x => x.BackgroundUpload)
            .Include(x => x.SituationUpload)
            .Include(x => x.ConclusionUpload)
            //TODO: INCLUDE AND THEN INCLUDE REST OF THE OBJS
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            return upload;
        }


    }
}