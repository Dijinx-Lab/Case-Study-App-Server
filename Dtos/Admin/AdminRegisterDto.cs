using System.ComponentModel.DataAnnotations;

namespace CaseStudyAppServer.Dtos.Admin
{
    public class AdminRegisterDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }
    }
}