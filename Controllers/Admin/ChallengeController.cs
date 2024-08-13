using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Challenges;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.ChallengeRoute)]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeRepository _challengeRepo;
        private readonly IUploadRepository _uploadRepo;
        private readonly ICaseStudyRepository _studyRepo;

        public ChallengeController(IChallengeRepository challengeRepo, IUploadRepository uploadRepo, ICaseStudyRepository studyRepo)
        {
            _challengeRepo = challengeRepo;
            _uploadRepo = uploadRepo;
            _studyRepo = studyRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ChallengeRequestDto requestDto)
        {
            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            Upload? upload = await _uploadRepo.GetByIdAsync(requestDto.UploadId);
            if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            Challenge? createdChallenge = await _challengeRepo.CreateAsync(requestDto.ToChallengeFromRequestDto());
            if (createdChallenge == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { challenge = createdChallenge.ToResponseDto() });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChallengeRequestDto requestDto)
        {
            var existingItem = await _challengeRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            if (existingItem.UploadId != requestDto.UploadId)
            {
                Upload? upload = await _uploadRepo.GetByIdAsync(requestDto.UploadId);
                if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);
                await _uploadRepo.DeleteAsync(existingItem.UploadId);
            }

            var updatedItem = await _challengeRepo.UpdateAsync(id, requestDto);

            if (updatedItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { challenge = updatedItem });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var existingFigure = await _challengeRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            await _uploadRepo.DeleteAsync(existingFigure.UploadId);
            await _challengeRepo.DeleteAsync(existingFigure.Id);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingItem = await _challengeRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { challenge = existingItem.ToResponseDto() });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int? id)
        {
            var items = await _challengeRepo.GetAllAsync(id);
            
            var filteredItems = items.Select(s => s.ToResponseDto());
            return ResponseUtility.ReturnOk(new { challenges = filteredItems });
        }
    }
}