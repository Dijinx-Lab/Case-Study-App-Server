using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Teams
{
    public class TeamRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

    }
}