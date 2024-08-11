using CaseStudyAppServer.Data;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDBContext _context;

        public TeamService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateUniqueTeamCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            string code;

            do
            {
                code = new string(Enumerable.Repeat(chars, 4)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (await CheckTeamCodeExists(code));

            return code;
        }

        public async Task<bool> CheckTeamCodeExists(string code)
        {
            return await _context.Teams.AnyAsync(x => x.Code.Equals(code));
        }
    }


}