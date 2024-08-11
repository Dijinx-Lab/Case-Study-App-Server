using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Team
{
    public class CreateTeamDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

    }
}