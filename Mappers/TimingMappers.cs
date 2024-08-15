
using CaseStudyAppServer.Dtos.Timings;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class TimingMappers
    {
        public static TimingResponseDto ToResponseDto(this Timing responseObj)
        {
            int? durationInSeconds = null;

            if (responseObj.StartedOn.HasValue && responseObj.EndedOn.HasValue)
            {
                durationInSeconds = (int)(responseObj.EndedOn.Value - responseObj.StartedOn.Value).TotalSeconds;
            }
            return new TimingResponseDto
            {
                Id = responseObj.Id,
                StartedOn = responseObj.StartedOn,
                EndedOn = responseObj.EndedOn,
                DurationInSeconds = durationInSeconds,
                CreatedOn = responseObj.CreatedOn,
                UpdatedOn = responseObj.UpdatedOn,
                DeletedOn = responseObj.DeletedOn,
            };
        }
    }
}