using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Models
{
    [Table("Outcomes")]
    public class Outcome
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UploadId { get; set; }
        public Upload? Upload { get; set; }
        public int? CaseStudyId { get; set; }
        public CaseStudy? CaseStudy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}