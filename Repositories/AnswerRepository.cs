using CaseStudyAppServer.Data;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDBContext _context;
        public AnswerRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Answer>> CreateBatchAsync(List<Answer> answer)
        {
            await _context.Answers.AddRangeAsync(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<List<Answer>> GetAllByQuestionIdAsync(int id)
        {
            var items = await _context.Answers
                    .Where(x => x.DeletedOn == null && x.QuestionId == id)
                    .ToListAsync();

            return items;
        }

        public async Task<List<Answer>> GetAllByTeamIdAsync(int id)
        {
            var items = await _context.Answers
                    .Where(x => x.DeletedOn == null && x.TeamId == id)
                    .ToListAsync();

            return items;
        }
    }
}