using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyAppServer.Models
{
    [Table("Tokens")]
    public class Token
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string RefreshValue { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}