using CaseStudyAppServer.Dtos.CaseStudies;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class CaseStudyMappers
    {
        public static CaseStudy ToObjectFromRequestDto(this CaseStudyRequestDto requestDto)
        {
            return new CaseStudy
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                CoverUploadId = requestDto.CoverUploadId,
                OverviewDescription = requestDto.OverviewDescription,
                OverviewUploadId = requestDto.OverviewUploadId,
                BackgroundDescription = requestDto.BackgroundDescription,
                BackgroundUploadId = requestDto.BackgroundUploadId,
                SituationDescription = requestDto.SituationDescription,
                SituationUploadId = requestDto.SituationUploadId,
                ConclusionDescription = requestDto.ConclusionDescription,
                ConclusionUploadId = requestDto.ConclusionUploadId,
            };
        }
        public static CaseStudyResponseDto ToResponseDto(this CaseStudy responseObj)
        {
            return new CaseStudyResponseDto
            {
                Id = responseObj.Id,
                Name = responseObj.Name,
                Description = responseObj.Description,
                OverviewDescription = responseObj.OverviewDescription,
                BackgroundDescription = responseObj.BackgroundDescription,
                SituationDescription = responseObj.SituationDescription,
                ConclusionDescription = responseObj.ConclusionDescription,
                CoverUpload = responseObj.CoverUpload,
                OverviewUpload = responseObj.OverviewUpload,
                BackgroundUpload = responseObj.BackgroundUpload,
                SituationUpload = responseObj.SituationUpload,
                ConclusionUpload = responseObj.ConclusionUpload,
                Challenges = responseObj.Challenges?.Select(x => x.ToDeleteSafe().ToResponseDto()).ToList(),
                Outcomes = responseObj.Outcomes?.Select(x => x.ToDeleteSafe().ToResponseDto()).ToList(),
                LeadershipStrategies = responseObj.LeadershipStrategies?.Select(x => x.ToResponseDto()).ToList(),
                CreatedOn = responseObj.CreatedOn,
                UpdatedOn = responseObj.UpdatedOn,
                DeletedOn = responseObj.DeletedOn,
            };
        }

        public static MinimalCaseStudyDto ToMinimalDto(this CaseStudy responseObj)
        {
            return new MinimalCaseStudyDto
            {
                Id = responseObj.Id,
                Name = responseObj.Name,
                Description = responseObj.Description,
                CreatedOn = responseObj.CreatedOn,
                UpdatedOn = responseObj.UpdatedOn,
                DeletedOn = responseObj.DeletedOn,
            };
        }
    }
}