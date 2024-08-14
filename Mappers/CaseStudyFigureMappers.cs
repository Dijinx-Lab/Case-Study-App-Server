using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.CaseStudyFigure;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class CaseStudyFigureMappers
    {
        public static CaseStudyFigure ToObjectFromRequestDto(this CaseStudyFigureRequestDto requestDto)
        {
            return new CaseStudyFigure
            {
                FigureId = requestDto.FigureId,
                CaseStudyId = requestDto.CaseStudyId,
            };
        }

    }
}