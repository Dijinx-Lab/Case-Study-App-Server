using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Challenges;
using CaseStudyAppServer.Dtos.Figures;
using CaseStudyAppServer.Dtos.LeadershipStrategy;
using CaseStudyAppServer.Dtos.Outcomes;
using CaseStudyAppServer.Dtos.Timings;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Dtos.CaseStudies
{
    public class CaseStudyUserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? OverviewDescription { get; set; }
        public string? BackgroundDescription { get; set; }
        public string? SituationDescription { get; set; }
        public string? ConclusionDescription { get; set; }
        public Upload? CoverUpload { get; set; }
        public Upload? OverviewUpload { get; set; }
        public Upload? BackgroundUpload { get; set; }
        public Upload? SituationUpload { get; set; }
        public Upload? ConclusionUpload { get; set; }
        public List<ChallengeResponseDto>? Challenges { get; set; } = [];
        public List<OutcomeResponseDto>? Outcomes { get; set; } = [];
        public List<LeadershipStrategyResponseDto>? LeadershipStrategies { get; set; } = [];
        public List<FigureResponseDto>? Figures { get; set; } = [];
        public TimingResponseDto? Timing { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}