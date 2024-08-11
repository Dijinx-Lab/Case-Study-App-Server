using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IUploadService
    {
        Task<Upload?> CreateUploadAsync(IFormFile file, string folder);
        Task<Upload?> DeleteUploadAsync(int id);
    }
}