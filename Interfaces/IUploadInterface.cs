using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IUploadRepository
    {
        Task<Upload?> GetByIdAsync(int id);
        Task<Upload> CreateAsync(string key, string filename, string extension, string url);
        Task<Upload?> DeleteAsync(int id);
    }
}