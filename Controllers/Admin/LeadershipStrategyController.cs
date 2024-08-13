using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.LeadershipStrategy;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.LeadershipStrategyRoute)]
    public class LeadershipStrategyController : ControllerBase
    {
        private readonly ILeadershipStrategyRepository _strategyRepo;
        private readonly IUploadRepository _uploadRepo;
        private readonly ICaseStudyRepository _studyRepo;

        public LeadershipStrategyController(ILeadershipStrategyRepository strategyRepo, IUploadRepository uploadRepo, ICaseStudyRepository studyRepo)
        {
            _strategyRepo = strategyRepo;
            _uploadRepo = uploadRepo;
            _studyRepo = studyRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] LeadershipStrategyRequestDto requestDto)
        {
            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            LeadershipStrategy? createdItem = await _strategyRepo.CreateAsync(requestDto.ToStrategyFromRequestDto());
            if (createdItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { leadershipStrategy = createdItem.ToResponseDto() });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] LeadershipStrategyRequestDto requestDto)
        {
            var existingItem = await _strategyRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            var updatedItem = await _strategyRepo.UpdateAsync(id, requestDto);

            if (updatedItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { leadershipStrategy = updatedItem.ToResponseDto() });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var existingFigure = await _strategyRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            await _strategyRepo.DeleteAsync(existingFigure.Id);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingItem = await _strategyRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { leadershipStrategy = existingItem.ToResponseDto() });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int? id)
        {
            var items = await _strategyRepo.GetAllAsync(id);

            var filteredItems = items.Select(s => s.ToResponseDto());
            return ResponseUtility.ReturnOk(new { leadershipStrategies = filteredItems });
        }
    }
}