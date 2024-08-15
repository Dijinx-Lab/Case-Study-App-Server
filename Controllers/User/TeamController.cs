using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CaseStudyAppServer.Controllers.User
{
    [ApiController]
    [Route(RouteConstants.UserTeamRoute)]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepo;
        public TeamController(ITeamRepository teamRepo)
        {
            _teamRepo = teamRepo;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (code.IsNullOrEmpty()) return ResponseUtility.ReturnBadRequest(MessageConstants.TeamCodeIsRequired);
            var existingTeam = await _teamRepo.GetByCodeAsync(code);
            if (existingTeam == null)
            {
                return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);
            }

            return ResponseUtility.ReturnOk(new { team = existingTeam.ToResponseDto() });
        }
    }
}