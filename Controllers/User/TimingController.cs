using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Timings;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.User
{
    [ApiController]
    [Route(RouteConstants.TimingRoute)]
    public class TimingController : ControllerBase
    {
        private readonly ITimingRepository _timingRepo;
        private readonly ITeamRepository _teamRepo;
        private readonly ICaseStudyRepository _studyRepo;

        public TimingController(ITimingRepository timingRepo, ITeamRepository teamRepo, ICaseStudyRepository studyRepo)
        {
            _timingRepo = timingRepo;
            _teamRepo = teamRepo;
            _studyRepo = studyRepo;
        }

        [HttpPost("start")]
        public async Task<IActionResult> MarkStartTime([FromBody] TimingRequestDto requestDto)
        {
            var team = await _teamRepo.GetByCodeAsync(requestDto.Code);
            if (team == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            if (DateTime.TryParse(requestDto.Time, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime utcDateTime))
            {
                var createdTiming = await _timingRepo.CreateAsync(team.Id, requestDto.CaseStudyId, utcDateTime);
                return ResponseUtility.ReturnOk(new { timing = createdTiming });
            }

            return ResponseUtility.ReturnBadRequest(MessageConstants.InvalidTimeFormat);
        }

        [HttpPost("end")]
        public async Task<IActionResult> MarkEndTime([FromBody] TimingRequestDto requestDto)
        {

            var team = await _teamRepo.GetByCodeAsync(requestDto.Code);
            if (team == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            if (DateTime.TryParse(requestDto.Time, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime utcDateTime))
            {

                var createdTiming = await _timingRepo.UpdateAsync(team.Id, requestDto.CaseStudyId, utcDateTime);
                if (createdTiming == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound + " | " + MessageConstants.ShouldBeAfterStartTime);
                return ResponseUtility.ReturnOk(new { timing = createdTiming });
            }

            return ResponseUtility.ReturnBadRequest(MessageConstants.InvalidTimeFormat);
        }
    }
}