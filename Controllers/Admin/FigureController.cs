using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Figure;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.FigureRoute)]
    public class FigureController : ControllerBase
    {
        private readonly IFigureRepository _figureRepo;
        private readonly IUploadRepository _uploadRepo;
        public FigureController(IFigureRepository figureRepo, IUploadRepository uploadRepo)
        {
            _figureRepo = figureRepo;
            _uploadRepo = uploadRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] FigureRequestDto figureDto)
        {
            Upload? upload = await _uploadRepo.GetByIdAsync(figureDto.UploadId);
            if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            Figure? createdFigure = await _figureRepo.CreateAsync(figureDto.ToFigureFromRequestDto());
            if (createdFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { figure = createdFigure.ToResponseDto() });

        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingFigure = await _figureRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { figure = existingFigure.ToResponseDto() });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var figureList = await _figureRepo.GetAllAsync();
            var filteredFigures = figureList.Select(s => s.ToResponseDto());
            return ResponseUtility.ReturnOk(new { figures = filteredFigures });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FigureRequestDto figureRequestDto)
        {
            var existingFigure = await _figureRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            if (existingFigure.UploadId != figureRequestDto.UploadId)
            {
                Upload? upload = await _uploadRepo.GetByIdAsync(figureRequestDto.UploadId);
                if (upload == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);
                await _uploadRepo.DeleteAsync(existingFigure.UploadId);
            }

            var updatedTeam = await _figureRepo.UpdateAsync(id, figureRequestDto);

            if (updatedTeam == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(new { team = updatedTeam });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var existingFigure = await _figureRepo.GetByIdAsync(id);
            if (existingFigure == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            await _uploadRepo.DeleteAsync(existingFigure.UploadId);
            await _figureRepo.DeleteAsync(existingFigure.Id);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }
    }
}