using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyAppServer.Models
{
    [Table("Figures")]
    public class Figure
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UploadId { get; set; }
        public Upload? Upload { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}