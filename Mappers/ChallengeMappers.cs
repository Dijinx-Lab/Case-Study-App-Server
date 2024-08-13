using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}