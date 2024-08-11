
namespace CaseStudyAppServer.Interfaces
{
    public  interface ITeamService
    {
        Task<string> GenerateUniqueTeamCode();
    }
}