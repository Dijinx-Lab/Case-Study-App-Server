using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Dtos.CaseStudyFigure
{
    public class CaseStudyFigureCompleteObjDto
    {
        public Figure? Figure { get; set; }
        public List<CaseStudy> CaseStudies { get; set; } = [];
    }
}