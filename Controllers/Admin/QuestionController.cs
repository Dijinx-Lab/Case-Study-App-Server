using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Questions;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.QuestionRoute)]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly ICaseStudyRepository _studyRepo;

        public QuestionController(IQuestionRepository questionRepo, ICaseStudyRepository studyRepo)
        {
            _questionRepo = questionRepo;
            _studyRepo = studyRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] QuestionRequestDto requestDto)
        {
            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            Question? createdOutcome = await _questionRepo.CreateAsync(requestDto.ToQuestionFromRequestDto());
            if (createdOutcome == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { question = createdOutcome });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] QuestionRequestDto requestDto)
        {
            var existingItem = await _questionRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            var updatedItem = await _questionRepo.UpdateAsync(id, requestDto);

            if (updatedItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { question = updatedItem });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existingItem = await _questionRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            await _questionRepo.DeleteAsync(existingItem.Id);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingItem = await _questionRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { question = existingItem });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int? id)
        {
            var items = await _questionRepo.GetAllAsync(id);
            return ResponseUtility.ReturnOk(new { questions = items });
        }
    }
}