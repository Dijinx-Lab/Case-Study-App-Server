using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Timings
{
    public class TimingRequestDto
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public int CaseStudyId { get; set; }
        [Required]
        public string Time { get; set; } = string.Empty;
    }
}