using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Questions;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class QuestionMappers
    {
        public static Question ToQuestionFromRequestDto(this QuestionRequestDto requestDto)
        {
            return new Question
            {
                Title = requestDto.Title,
                Description = requestDto.Description,
                CaseStudyId = requestDto.CaseStudyId,
            };
        }
    }
}