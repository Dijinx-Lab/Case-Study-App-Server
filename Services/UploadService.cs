using System.Text.RegularExpressions;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Services
{
    public class UploadService : IUploadService
    {
        private readonly S3Utility _s3Util;
        private readonly IUploadRepository _uploadRepo;

        public UploadService(S3Utility s3Util, IUploadRepository uploadRepo)
        {
            _s3Util = s3Util;
            _uploadRepo = uploadRepo;
        }
        public async Task<Upload?> CreateUploadAsync(IFormFile file, string folder)
        {
            string fileName = DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ss");
            fileName = Regex.Replace(fileName, "[:.]", "-");
            var ext = Path.GetExtension(file.FileName);
            string key = string.IsNullOrEmpty(folder) ? $"{fileName}{ext}" : $"{folder}/{fileName}{ext}";

            var accessUrl = await _s3Util.UploadToS3Async(file, key);

            if (accessUrl == null) return null;

            var upload = await _uploadRepo.CreateAsync(key, fileName, ext, accessUrl);

            return upload;
        }

        public async Task<Upload?> DeleteUploadAsync(int id)
        {
            var upload = await _uploadRepo.GetByIdAsync(id);
            if (upload == null) return null;

            await _s3Util.DeleteFromS3Async(upload.Key);

            return await _uploadRepo.DeleteAsync(id);
        }
    }
}