using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Teams;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class TeamMappers
    {
        public static TeamResponseDto ToResponseDto(this Team responseObj)
        {
            return new TeamResponseDto
            {
                Id = responseObj.Id,
                Name = responseObj.Name,
                Code = responseObj.Code,
                CreatedOn = responseObj.CreatedOn,
                UpdatedOn = responseObj.UpdatedOn,
                DeletedOn = responseObj.DeletedOn,
            };
        }
    }
}