using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyAppServer.Models
{
    [Table("Uploads")]
    public class Upload
    {
        public int Id { get; set; }
        public string AccessUrl { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}