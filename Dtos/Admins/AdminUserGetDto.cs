using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Admins
{
    public class AdminUserGetDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}