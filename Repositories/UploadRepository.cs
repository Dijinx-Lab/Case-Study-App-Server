using CaseStudyAppServer.Data;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class UploadRepository : IUploadRepository
    {
        private readonly ApplicationDBContext _context;

        public UploadRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Upload> CreateAsync(string key, string filename, string extension, string url)
        {
            var upload = new Upload
            {
                AccessUrl = url,
                Extension = extension,
                Key = key,
                Filename = filename,
            };
            await _context.Uploads.AddAsync(upload);
            await _context.SaveChangesAsync();
            return upload;
        }

        public async Task<Upload?> DeleteAsync(int id)
        {
            var existingUpload = await _context.Uploads.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUpload == null) return null;

            existingUpload.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingUpload;
        }

        public async Task<Upload?> GetByIdAsync(int id)
        {
            var existingUpload = await _context.Uploads
           .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            return existingUpload;
        }
    }
}