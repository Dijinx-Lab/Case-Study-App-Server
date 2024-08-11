using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.Team;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using CaseStudyAppServer.Services;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ITeamService _teamService;

        public TeamRepository(ApplicationDBContext context, ITeamService teamService)
        {
            _context = context;
            _teamService = teamService;
        }

        public async Task<Team> CreateAsync(string name)
        {
            string uniqueCode = await _teamService.GenerateUniqueTeamCode();
            Team team = new()
            {
                Name = name,
                Code = uniqueCode
            };
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team?> DeleteAsync(string teamCode)
        {
            var existingTeam = await _context.Teams.FirstOrDefaultAsync(x => x.Code == teamCode && x.DeletedOn == null);
            if (existingTeam == null) return null;

            existingTeam.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTeam;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams
            .Where(t => t.DeletedOn == null)
            .ToListAsync();
        }

        public async Task<Team?> GetByCodeAsync(string teamCode)
        {
            var existingTeam = await _context.Teams
            .FirstOrDefaultAsync(x => x.Code == teamCode && x.DeletedOn == null);
            return existingTeam;
        }


        public async Task<Team?> UpdateAsync(string code, string name)
        {
            var existingTeam = await _context.Teams.FirstOrDefaultAsync(x => x.Code == code && x.DeletedOn == null);
            if (existingTeam == null) return null;

            existingTeam.Name = name;
            existingTeam.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTeam;
        }
    }
}