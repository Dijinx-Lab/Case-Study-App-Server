using CaseStudyAppServer.Dtos.Challenges;
using CaseStudyAppServer.Dtos.LeadershipStrategy;
using CaseStudyAppServer.Dtos.Outcomes;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Dtos.CaseStudies
{
    public class CaseStudyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Upload? CoverUpload { get; set; }
        public string? OverviewDescription { get; set; }
        public Upload? OverviewUpload { get; set; }
        public string? BackgroundDescription { get; set; }
        public Upload? BackgroundUpload { get; set; }
        public string? SituationDescription { get; set; }
        public Upload? SituationUpload { get; set; }
        public string? ConclusionDescription { get; set; }
        public Upload? ConclusionUpload { get; set; }
        public List<ChallengeResponseDto>? Challenges { get; set; } = [];
        public List<OutcomeResponseDto>? Outcomes { get; set; } = [];
        public List<LeadershipStrategyResponseDto>? LeadershipStrategies { get; set; } = [];
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}