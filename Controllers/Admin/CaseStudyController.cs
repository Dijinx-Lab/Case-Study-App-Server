using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.CaseStudies;
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
        private readonly ICaseStudyFigureRepository _caseFigureRepo;
        private readonly IChallengeRepository _challengeRepo;
        private readonly ILeadershipStrategyRepository _strategyRepo;
        private readonly IOutcomeRepository _outcomeRepo;
        public CaseStudyController(
            ICaseStudyRepository studyRepo, IUploadRepository uploadRepo,
            ICaseStudyFigureRepository caseFigureRepo, IChallengeRepository challengeRepo,
            ILeadershipStrategyRepository strategyRepo, IOutcomeRepository outcomeRepo)
        {
            _studyRepo = studyRepo;
            _uploadRepo = uploadRepo;
            _caseFigureRepo = caseFigureRepo;
            _challengeRepo = challengeRepo;
            _strategyRepo = strategyRepo;
            _outcomeRepo = outcomeRepo;
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

            CaseStudy? createdItem = await _studyRepo.CreateAsync(requestDto.ToObjectFromRequestDto());
            if (createdItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            return ResponseUtility.ReturnOk(new { casestudy = createdItem.ToResponseDto() });
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

            CaseStudy? updatedItem = await _studyRepo.UpdateAsync(id, requestDto);
            if (updatedItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ErrorProcessingRequest);

            existingItem = await _studyRepo.GetByIdAsync(id);

            var dtoObj = existingItem!.ToResponseDto();
            List<Figure> figures = await _caseFigureRepo.GetByCaseStudyIdAsync(dtoObj.Id);
            var filteredFigures = figures.Select(x => x.ToDeleteSafe().ToResponseDto());
            dtoObj.Figures = filteredFigures.ToList();

            return ResponseUtility.ReturnOk(new { casestudy = dtoObj });
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            CaseStudy? existingItem = await _studyRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            var dtoObj = existingItem.ToResponseDto();
            List<Figure> figures = await _caseFigureRepo.GetByCaseStudyIdAsync(dtoObj.Id);
            var filteredFigures = figures.Select(x => x.ToDeleteSafe().ToResponseDto());
            dtoObj.Figures = filteredFigures.ToList();

            return ResponseUtility.ReturnOk(new { casestudy = dtoObj });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _studyRepo.GetAllAsync();
            var filteredItems = new List<CaseStudyResponseDto>();

            foreach (var s in items)
            {
                var dtoObj = s.ToResponseDto();
                List<Figure> figures = await _caseFigureRepo.GetByCaseStudyIdAsync(dtoObj.Id);
                var filteredFigures = figures.Select(x => x.ToDeleteSafe().ToResponseDto());
                dtoObj.Figures = filteredFigures.ToList();
                filteredItems.Add(dtoObj);
            }

            return ResponseUtility.ReturnOk(new { casestudies = filteredItems });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existingItem = await _studyRepo.GetByIdAsync(id);
            if (existingItem == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            List<Figure> figures = await _caseFigureRepo.GetByCaseStudyIdAsync(id);

            List<int?> uploadIds = [existingItem.CoverUploadId, existingItem.OverviewUploadId, existingItem.BackgroundUploadId, existingItem.SituationUploadId, existingItem.ConclusionUploadId];
            uploadIds.AddRange(existingItem.Challenges.Select(x => (int?)x.UploadId));
            uploadIds.AddRange(existingItem.Outcomes.Select(x => (int?)x.UploadId));

            foreach (var uploadId in uploadIds)
            {
                if (uploadId != null)
                {
                    await _uploadRepo.DeleteAsync(uploadId.Value);
                }
            }

            await _challengeRepo.DeleteByCaseStudyIdAsync(id);
            await _outcomeRepo.DeleteByCaseStudyIdAsync(id);
            await _strategyRepo.DeleteByCaseStudyIdAsync(id);

            foreach (var figure in figures)
            {
                if (figure != null)
                {
                    await _caseFigureRepo.DeleteAsync(id, figure.Id);
                }
            }

            await _studyRepo.DeleteAsync(id);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }
    }

}