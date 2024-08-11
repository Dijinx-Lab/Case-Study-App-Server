using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Dtos.Admin
{
    public class AdminUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string AuthToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}