using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.CaseStudies;
using CaseStudyAppServer.Dtos.CaseStudyFigure;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [ApiController]
    [Route(RouteConstants.CaseStudyFigureRoute)]
    public class CaseStudyFigureController : ControllerBase
    {
        private readonly ICaseStudyFigureRepository _caseFigureRepo;
        private readonly ICaseStudyRepository _studyRepo;
        private readonly IFigureRepository _figureRepo;
        public CaseStudyFigureController(ICaseStudyFigureRepository caseFigureRepo, ICaseStudyRepository studyRepo, IFigureRepository figureRepo)
        {
            _studyRepo = studyRepo;
            _caseFigureRepo = caseFigureRepo;
            _figureRepo = figureRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CaseStudyFigureRequestDto requestDto)
        {
            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            var figure = await _figureRepo.GetByIdAsync(requestDto.FigureId);
            if (figure == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            CaseStudyFigure? createdItem = await _caseFigureRepo.CreateAsync(requestDto.ToObjectFromRequestDto());
            if (createdItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(null, "");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] CaseStudyFigureRequestDto requestDto)
        {
            bool caseStudyExists = await _studyRepo.CheckExistsAsync(requestDto.CaseStudyId);
            if (!caseStudyExists) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            var figure = await _figureRepo.GetByIdAsync(requestDto.FigureId);
            if (figure == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);

            CaseStudyFigure? createdItem = await _caseFigureRepo.DeleteAsync(requestDto.CaseStudyId, requestDto.FigureId);
            if (createdItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromRoute] int id, [FromQuery] bool? figure)
        {
            bool forFigure = figure ?? false;
            dynamic response;
            if (forFigure)
            {
                var figureObj = await _figureRepo.GetByIdAsync(id);
                if (figureObj == null) return ResponseUtility.ReturnBadRequest(MessageConstants.ItemNotFound);
                List<CaseStudy> caseStudies = await _caseFigureRepo.GetByFigureIdAsync(id);
                var filteredItems = caseStudies.Select(x => x.ToMinimalDto());
                response = new { figure = figureObj.ToResponseDto(), casestudies = filteredItems };
            }
            else
            {
                List<Figure> figures = await _caseFigureRepo.GetByCaseStudyIdAsync(id);
                var filteredItems = figures.Select(x => x.ToResponseDto());
                response = new { figures = filteredItems };
            }

            return ResponseUtility.ReturnOk(response);
        }
    }
}