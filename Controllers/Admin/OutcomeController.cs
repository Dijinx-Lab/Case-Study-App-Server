using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Outcomes;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.OutcomeRoute)]
    public class OutcomeController : ControllerBase
    {
        private readonly IOutcomeRepository _outcomeRepo;
        private readonly IUploadRepository _uploadRepo;
        private readonly ICaseStudyRepository _studyRepo;

        public OutcomeController(IOutcomeRepository outcomeRepo, IUploadRepository uploadRepo, ICaseStudyRepository studyRepo)
        {
            _outcomeRepo = outcomeRepo;
            _uploadRepo = uploadRepo;
            _studyRepo = studyRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] OutcomeRequestDto requestDto)
        {
            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            Upload? upload = await _uploadRepo.GetByIdAsync(requestDto.UploadId);
            if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            Outcome? createdOutcome = await _outcomeRepo.CreateAsync(requestDto.ToOutcomeFromRequestDto());
            if (createdOutcome == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { outcome = createdOutcome.ToResponseDto() });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OutcomeRequestDto requestDto)
        {
            var existingItem = await _outcomeRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            if (existingItem.UploadId != requestDto.UploadId)
            {
                Upload? upload = await _uploadRepo.GetByIdAsync(requestDto.UploadId);
                if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);
                await _uploadRepo.DeleteAsync(existingItem.UploadId);
            }

            var updatedItem = await _outcomeRepo.UpdateAsync(id, requestDto);

            if (updatedItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { outcome = updatedItem.ToResponseDto() });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var existingFigure = await _outcomeRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            await _uploadRepo.DeleteAsync(existingFigure.UploadId);
            await _outcomeRepo.DeleteAsync(existingFigure.Id);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingItem = await _outcomeRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { outcome = existingItem.ToResponseDto() });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int? id)
        {
            var items = await _outcomeRepo.GetAllAsync(id);

            var filteredItems = items.Select(s => s.ToResponseDto());
            return ResponseUtility.ReturnOk(new { outcomes = filteredItems });
        }
    }
}