using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Answers
{
    public class AnswerRequestDto
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public List<MinimalAnswerRequestDto> Answers { get; set; } = [];
    }
}