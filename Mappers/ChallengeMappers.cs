using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Challenge;
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
            };
        }
    }
}