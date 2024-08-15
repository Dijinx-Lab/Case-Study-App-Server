using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyAppServer.Models
{
    [Table("CaseStudies")]
    public class CaseStudy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CoverUploadId { get; set; }
        public Upload? CoverUpload { get; set; }
        public string? OverviewDescription { get; set; }
        public int? OverviewUploadId { get; set; }
        public Upload? OverviewUpload { get; set; }
        public string? BackgroundDescription { get; set; }
        public int? BackgroundUploadId { get; set; }
        public Upload? BackgroundUpload { get; set; }
        public string? SituationDescription { get; set; }
        public int? SituationUploadId { get; set; }
        public Upload? SituationUpload { get; set; }
        public string? ConclusionDescription { get; set; }
        public int? ConclusionUploadId { get; set; }
        public Upload? ConclusionUpload { get; set; }
        public List<CaseStudyFigure> CaseStudyFigures { get; set; } = [];
        public List<Challenge> Challenges { get; set; } = [];
        public List<LeadershipStrategy> LeadershipStrategies { get; set; } = [];
        public List<Outcome> Outcomes { get; set; } = [];

        public List<Question> Questions { get; set; } = [];

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}