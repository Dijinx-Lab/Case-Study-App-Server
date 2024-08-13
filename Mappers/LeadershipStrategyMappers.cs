using CaseStudyAppServer.Dtos.LeadershipStrategy;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class LeadershipStrategyMappers
    {
        public static LeadershipStrategy ToStrategyFromRequestDto(this LeadershipStrategyRequestDto requestDto)
        {
            return new LeadershipStrategy
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                CaseStudyId = requestDto.CaseStudyId,
            };
        }

        public static LeadershipStrategyResponseDto ToResponseDto(this LeadershipStrategy responseObj)
        {
            return new LeadershipStrategyResponseDto
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