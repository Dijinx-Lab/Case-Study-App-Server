using System.ComponentModel.DataAnnotations;

namespace CaseStudyAppServer.Dtos.CaseStudy
{
    public class CaseStudyRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int CoverUploadId { get; set; }
        public string? OverviewDescription { get; set; }
        public int? OverviewUploadId { get; set; }
        public string? BackgroundDescription { get; set; }
        public int? BackgroundUploadId { get; set; }
        public string? SituationDescription { get; set; }
        public int? SituationUploadId { get; set; }
        public string? ConclusionDescription { get; set; }
        public int? ConclusionUploadId { get; set; }
    }
}