using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                CoverUpload = responseObj.CoverUpload,
                OverviewDescription = responseObj.OverviewDescription,
                OverviewUpload = responseObj.OverviewUpload,
                BackgroundDescription = responseObj.BackgroundDescription,
                BackgroundUpload = responseObj.BackgroundUpload,
                SituationDescription = responseObj.SituationDescription,
                SituationUpload = responseObj.SituationUpload,
                ConclusionDescription = responseObj.ConclusionDescription,
                ConclusionUpload = responseObj.ConclusionUpload,
                Challenges = responseObj.Challenges?.Select(x => x.ToResponseDto()).ToList(),
                CreatedOn = responseObj.CreatedOn,
                UpdatedOn = responseObj.UpdatedOn,
                DeletedOn = responseObj.DeletedOn,
            };
        }
    }
}