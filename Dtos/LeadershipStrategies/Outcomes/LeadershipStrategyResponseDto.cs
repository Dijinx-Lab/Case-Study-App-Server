using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Dtos.LeadershipStrategy
{
    public class LeadershipStrategyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}