using Microsoft.AspNetCore.Identity;

namespace CaseStudyAppServer.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}