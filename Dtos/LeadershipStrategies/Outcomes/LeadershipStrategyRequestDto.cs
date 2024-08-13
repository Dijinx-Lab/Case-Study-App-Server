using System.ComponentModel.DataAnnotations;

namespace CaseStudyAppServer.Dtos.LeadershipStrategy
{
    public class LeadershipStrategyRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int CaseStudyId { get; set; }
    }
}