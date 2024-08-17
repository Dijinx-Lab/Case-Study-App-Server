using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Interfaces
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> GetAllByTeamIdAsync(int id);
        Task<List<Answer>> GetAllByQuestionIdAsync(int id);
        Task<List<Answer>> CreateBatchAsync(List<Answer> answers);
    }
}