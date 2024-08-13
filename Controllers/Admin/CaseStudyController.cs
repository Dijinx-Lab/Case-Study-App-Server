using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.CaseStudy;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.CaseStudyRoute)]
    public class CaseStudyController : ControllerBase
    {
        private readonly ICaseStudyRepository _studyRepo;
        private readonly IUploadRepository _uploadRepo;
        public CaseStudyController(ICaseStudyRepository studyRepo, IUploadRepository uploadRepo)
        {
            _studyRepo = studyRepo;
            _uploadRepo = uploadRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CaseStudyRequestDto requestDto)
        {
            List<int?> uploadIds = [requestDto.CoverUploadId, requestDto.OverviewUploadId, requestDto.BackgroundUploadId, requestDto.SituationUploadId, requestDto.ConclusionUploadId];

            foreach (int? uploadId in uploadIds)
            {
                if (uploadId != null)
                {
                    Upload? upload = await _uploadRepo.GetByIdAsync(uploadId.Value);
                    if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);
                }
            }

            //TODO: ADD MORE OBJ CHECKS HERE

            CaseStudy? createdItem = await _studyRepo.CreateAsync(requestDto.ToObjectFromRequestDto());
            if (createdItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { caseStudy = createdItem.ToResponseDto() });

        }
    }
}