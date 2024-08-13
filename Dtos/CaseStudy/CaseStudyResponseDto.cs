using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Dtos.CaseStudy
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
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}