using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Dtos.Questions;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllAsync(int? caseStudyId);
        Task<Question?> GetByIdAsync(int id);
        Task<Question> CreateAsync(Question question);
        Task<Question?> UpdateAsync(int id, QuestionRequestDto requestDto);
        Task<Question?> DeleteAsync(int id);
        Task DeleteByCaseStudyIdAsync(int id);
    }
}