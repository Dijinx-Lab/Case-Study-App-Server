using System.ComponentModel.DataAnnotations;

namespace CaseStudyAppServer.Dtos.Outcomes
{
    public class OutcomeRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int UploadId { get; set; }
        [Required]
        public int CaseStudyId { get; set; }
    }
}