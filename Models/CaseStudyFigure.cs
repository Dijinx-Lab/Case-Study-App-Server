using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Models
{
    [Table("CaseStudyFigures")]
    public class CaseStudyFigure
    {

        public int FigureId { get; set; }

        [ForeignKey("FigureId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Figure Figure { get; set; }
        public int CaseStudyId { get; set; }

        [ForeignKey("CaseStudyId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CaseStudy CaseStudy { get; set; }

    }
}