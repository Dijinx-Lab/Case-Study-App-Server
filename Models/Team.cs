using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyAppServer.Models
{
    [Table("Teams")]
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<Timing> Timings { get; set; } = [];
        public List<Answer> Answers { get; set; } = [];
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}