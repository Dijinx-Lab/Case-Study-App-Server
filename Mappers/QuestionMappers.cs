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

        public static QuestionResponseDto ToResponse(this Question obj)
        {
            return new QuestionResponseDto
            {
                Id = obj.Id,
                Title = obj.Title,
                Description = obj.Description,
                CreatedOn = obj.CreatedOn,
                UpdatedOn = obj.UpdatedOn,
                DeletedOn = obj.DeletedOn,
            };
        }

        public static QuestionAnswerResponseDto ToResponseWithAnswer(this Question obj)
        {
            return new QuestionAnswerResponseDto
            {
                Id = obj.Id,
                Title = obj.Title,
                Description = obj.Description,
                CreatedOn = obj.CreatedOn,
                UpdatedOn = obj.UpdatedOn,
                DeletedOn = obj.DeletedOn,
            };
        }
    }
}