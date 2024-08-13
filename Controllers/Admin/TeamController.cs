using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Teams;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.TeamRoute)]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepo;
        public TeamController(ITeamRepository teamRepo)
        {
            _teamRepo = teamRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TeamRequestDto createTeamDto)
        {
            if (string.IsNullOrEmpty(createTeamDto.Name)) return ResponseUtility.ReturnBadRequest(MessageConstants.NameIsRequired);
            Team? createdTeam = await _teamRepo.CreateAsync(createTeamDto.Name);
            createdTeam = await _teamRepo.GetByCodeAsync(createdTeam.Code);

            if (createdTeam == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { team = createdTeam });

        }

        [HttpGet("{code}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (code.IsNullOrEmpty()) return ResponseUtility.ReturnBadRequest(MessageConstants.TeamCodeIsRequired);
            var existingTeam = await _teamRepo.GetByCodeAsync(code);
            if (existingTeam == null)
            {
                return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);
            }
            else
            {
                return ResponseUtility.ReturnOk(new { team = existingTeam });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var teamsList = await _teamRepo.GetAllAsync();
            return ResponseUtility.ReturnOk(new { teams = teamsList });
        }

        [HttpPut("{code}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] string code, [FromBody] TeamRequestDto createTeamDto)
        {
            if (createTeamDto.Name.IsNullOrEmpty()) return ResponseUtility.ReturnBadRequest(MessageConstants.NameIsRequired);

            var updatedTeam = await _teamRepo.UpdateAsync(code, createTeamDto.Name);

            if (updatedTeam == null)
            {
                return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);
            }
            else
            {
                return ResponseUtility.ReturnOk(new { team = updatedTeam });
            }
        }

        [HttpDelete("{code}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] string code)
        {

            var team = await _teamRepo.DeleteAsync(code);
            if (team == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }
    }
}