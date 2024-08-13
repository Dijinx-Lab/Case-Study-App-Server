using CaseStudyAppServer.Dtos.Outcomes;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class OutcomeMappers
    {
        public static Outcome ToOutcomeFromRequestDto(this OutcomeRequestDto requestDto)
        {
            return new Outcome
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                UploadId = requestDto.UploadId,
                CaseStudyId = requestDto.CaseStudyId,
            };
        }

        public static OutcomeResponseDto ToResponseDto(this Outcome responseObj)
        {
            return new OutcomeResponseDto
            {
                Id = responseObj.Id,
                Name = responseObj.Name,
                Description = responseObj.Description,
                Upload = responseObj.Upload,
                CreatedOn = responseObj.CreatedOn,
                UpdatedOn = responseObj.UpdatedOn,
                DeletedOn = responseObj.DeletedOn,
            };
        }

        public static Outcome ToDeleteSafe(this Outcome outcome)
        {
            return new Outcome
            {
                Id = outcome.Id,
                Name = outcome.Name,
                Description = outcome.Description,
                UploadId = outcome.UploadId,
                Upload = outcome.Upload == null ? null : outcome.Upload.DeletedOn != null ? null : outcome.Upload,
                CaseStudyId = outcome.CaseStudyId,
                CaseStudy = outcome.CaseStudy,
                CreatedOn = outcome.CreatedOn,
                UpdatedOn = outcome.UpdatedOn,
                DeletedOn = outcome.DeletedOn,
            };
        }
    }
}