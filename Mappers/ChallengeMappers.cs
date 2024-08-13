using CaseStudyAppServer.Dtos.Challenges;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class ChallengeMappers
    {
        public static Challenge ToChallengeFromRequestDto(this ChallengeRequestDto requestDto)
        {
            return new Challenge
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                UploadId = requestDto.UploadId,
                CaseStudyId = requestDto.CaseStudyId,
            };
        }

        public static ChallengeResponseDto ToResponseDto(this Challenge responseObj)
        {
            return new ChallengeResponseDto
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

        public static Challenge ToDeleteSafe(this Challenge challenge)
        {
            return new Challenge
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Description = challenge.Description,
                UploadId = challenge.UploadId,
                Upload = challenge.Upload == null ? null : challenge.Upload.DeletedOn != null ? null : challenge.Upload,
                CaseStudyId = challenge.CaseStudyId,
                CaseStudy = challenge.CaseStudy,
                CreatedOn = challenge.CreatedOn,
                UpdatedOn = challenge.UpdatedOn,
                DeletedOn = challenge.DeletedOn,
            };
        }
    }
}