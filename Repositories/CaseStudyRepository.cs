using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.CaseStudies;
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

            existingItem.OverviewDescription = caseStudyRequestDto.OverviewDescription;
            existingItem.BackgroundDescription = caseStudyRequestDto.BackgroundDescription;
            existingItem.SituationDescription = caseStudyRequestDto.SituationDescription;
            existingItem.ConclusionDescription = caseStudyRequestDto.ConclusionDescription;

            existingItem.OverviewUploadId = caseStudyRequestDto.OverviewUploadId;
            existingItem.BackgroundUploadId = caseStudyRequestDto.BackgroundUploadId;
            existingItem.SituationUploadId = caseStudyRequestDto.SituationUploadId;
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
                .Include(x => x.LeadershipStrategies)
                .Select(x => new
                {
                    Entity = x,
                    CoverUpload = x.CoverUpload != null && x.CoverUpload.DeletedOn == null ? x.CoverUpload : null,
                    OverviewUpload = x.OverviewUpload != null && x.OverviewUpload.DeletedOn == null ? x.OverviewUpload : null,
                    BackgroundUpload = x.BackgroundUpload != null && x.BackgroundUpload.DeletedOn == null ? x.BackgroundUpload : null,
                    SituationUpload = x.SituationUpload != null && x.SituationUpload.DeletedOn == null ? x.SituationUpload : null,
                    ConclusionUpload = x.ConclusionUpload != null && x.ConclusionUpload.DeletedOn == null ? x.ConclusionUpload : null,
                    Challenges = x.Challenges
                        .Where(c => c.DeletedOn == null)
                        .Select(c => new
                        {
                            Challenge = c,
                            Upload = c.Upload
                        })
                        .ToList(),
                    Outcomes = x.Outcomes
                        .Where(o => o.DeletedOn == null)
                        .Select(o => new
                        {
                            Outcome = o,
                            Upload = o.Upload
                        })
                        .ToList()
                })
                .ToListAsync();

            uploads.ForEach(x =>
            {
                x.Entity.Challenges = x.Challenges.Select(c =>
                {
                    c.Challenge.Upload = c.Upload;
                    return c.Challenge;
                }).ToList();
                x.Entity.Outcomes = x.Outcomes.Select(o =>
                {
                    o.Outcome.Upload = o.Upload;
                    return o.Outcome;
                }).ToList();
            });

            return uploads.Select(x => x.Entity).ToList();
        }

        public async Task<CaseStudy?> GetByIdAsync(int id)
        {
            var item = await _context.CaseStudies
                .Where(x => x.Id == id && x.DeletedOn == null)
                .Include(x => x.CoverUpload)
                .Include(x => x.OverviewUpload)
                .Include(x => x.BackgroundUpload)
                .Include(x => x.SituationUpload)
                .Include(x => x.ConclusionUpload)
                .Include(x => x.LeadershipStrategies)
                .Select(x => new
                {
                    Entity = x,
                    CoverUpload = x.CoverUpload != null && x.CoverUpload.DeletedOn == null ? x.CoverUpload : null,
                    OverviewUpload = x.OverviewUpload != null && x.OverviewUpload.DeletedOn == null ? x.OverviewUpload : null,
                    BackgroundUpload = x.BackgroundUpload != null && x.BackgroundUpload.DeletedOn == null ? x.BackgroundUpload : null,
                    SituationUpload = x.SituationUpload != null && x.SituationUpload.DeletedOn == null ? x.SituationUpload : null,
                    ConclusionUpload = x.ConclusionUpload != null && x.ConclusionUpload.DeletedOn == null ? x.ConclusionUpload : null,
                    Challenges = x.Challenges
                        .Where(c => c.DeletedOn == null)
                        .Select(c => new
                        {
                            Challenge = c,
                            Upload = c.Upload != null && c.Upload.DeletedOn == null ? c.Upload : null
                        })
                        .ToList(),
                    Outcomes = x.Outcomes
                        .Where(o => o.DeletedOn == null)
                        .Select(o => new
                        {
                            Outcome = o,
                            Upload = o.Upload != null && o.Upload.DeletedOn == null ? o.Upload : null
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return null;
            }

            // Reassign filtered uploads to the entity
            item.Entity.CoverUpload = item.CoverUpload;
            item.Entity.OverviewUpload = item.OverviewUpload;
            item.Entity.BackgroundUpload = item.BackgroundUpload;
            item.Entity.SituationUpload = item.SituationUpload;
            item.Entity.ConclusionUpload = item.ConclusionUpload;
            item.Entity.Challenges = item.Challenges.Select(c =>
            {
                c.Challenge.Upload = c.Upload;
                return c.Challenge;
            }).ToList();
            item.Entity.Outcomes = item.Outcomes.Select(o =>
            {
                o.Outcome.Upload = o.Upload;
                return o.Outcome;
            }).ToList();

            return item.Entity;
        }

        public async Task<bool> CheckExistsAsync(int id)
        {
            var item = await _context.CaseStudies.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            return item != null;
        }
    }
}