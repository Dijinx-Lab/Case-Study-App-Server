using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.CaseStudies;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CaseStudyAppServer.Controllers.User
{
    [ApiController]
    [Route(RouteConstants.UserCaseStudyRoute)]
    public class CaseStudyController : ControllerBase
    {
        private readonly ICaseStudyRepository _studyRepo;
        private readonly ITimingRepository _timingRepo;
        private readonly ICaseStudyFigureRepository _caseFigureRepo;
        private readonly ITeamRepository _teamRepo;
        public CaseStudyController(
            ICaseStudyRepository studyRepo, ITimingRepository timingRepo,
            ICaseStudyFigureRepository caseFigureRepo, ITeamRepository teamRepo)
        {
            _studyRepo = studyRepo;
            _timingRepo = timingRepo;
            _caseFigureRepo = caseFigureRepo;
            _teamRepo = teamRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string code)
        {
            if (code.IsNullOrEmpty()) return ResponseUtility.ReturnBadRequest(MessageConstants.TeamCodeIsRequired);
            var existingTeam = await _teamRepo.GetByCodeAsync(code);
            if (existingTeam == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);
            var items = await _studyRepo.GetAllAsync();
            var filteredItems = new List<CaseStudyUserResponseDto>();
            List<Timing> teamTimings = await _timingRepo.GetAllByTeamIdAsync(existingTeam.Id);

            foreach (var s in items)
            {
                var dtoObj = s.ToUserResponseDto();
                List<Figure> figures = await _caseFigureRepo.GetByCaseStudyIdAsync(dtoObj.Id);
                var filteredFigures = figures.Select(x => x.ToDeleteSafe().ToResponseDto());
                dtoObj.Figures = filteredFigures.ToList();

                Timing? caseStudyTiming = teamTimings.FirstOrDefault(x => x.CaseStudyId == s.Id);

                if (caseStudyTiming != null) dtoObj.Timing = caseStudyTiming.ToResponseDto();

                filteredItems.Add(dtoObj);
            }

            return ResponseUtility.ReturnOk(new { casestudies = filteredItems });
        }

    }
}