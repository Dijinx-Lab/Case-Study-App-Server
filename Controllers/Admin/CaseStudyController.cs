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

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CaseStudyRequestDto requestDto)
        {
            var existingItem = await _studyRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            List<int?> newUploadIds = [requestDto.CoverUploadId, requestDto.OverviewUploadId, requestDto.BackgroundUploadId, requestDto.SituationUploadId, requestDto.ConclusionUploadId];
            List<int?> existingUploadIds = [existingItem.CoverUploadId, existingItem.OverviewUploadId, existingItem.BackgroundUploadId, existingItem.SituationUploadId, existingItem.ConclusionUploadId];

            for (int i = 0; i < newUploadIds.Count; i++)
            {
                if (existingUploadIds[i] != newUploadIds[i])
                {
                    if (newUploadIds[i] != null)
                    {
                        Upload? upload = await _uploadRepo.GetByIdAsync(newUploadIds[i]!.Value);
                        if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);
                    }
                    if (existingUploadIds[i] != null)
                    {
                        await _uploadRepo.DeleteAsync(existingUploadIds[i]!.Value);
                    }
                }
            }

            //TODO: ADD MORE OBJ CHECKS HERE

            CaseStudy? updatedItem = await _studyRepo.UpdateAsync(id, requestDto);
            if (updatedItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { caseStudy = updatedItem.ToResponseDto() });
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingFigure = await _studyRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { caseStudy = existingFigure.ToResponseDto() });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var figureList = await _studyRepo.GetAllAsync();
            var filteredFigures = figureList.Select(s => s.ToResponseDto());
            return ResponseUtility.ReturnOk(new { caseStudies = filteredFigures });
        }
    }
}