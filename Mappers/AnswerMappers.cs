using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Answers;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class AnswerMappers
    {
        public static Answer ToAnswerFromRequestDto(this MinimalAnswerRequestDto requestDto)
        {
            return new Answer
            {
                QuestionId = requestDto.QuestionId,
                Description = requestDto.Answer,
            };
        }
    }
}