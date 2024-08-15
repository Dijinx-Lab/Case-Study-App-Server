using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.Questions;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyAppServer.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDBContext _context;

        public QuestionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Question> CreateAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<Question?> DeleteAsync(int id)
        {
            var existingItem = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<Question?> UpdateAsync(int id, QuestionRequestDto requestDto)
        {
            var existingItem = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
            if (existingItem == null) return null;

            existingItem.Title = requestDto.Title;
            existingItem.Description = requestDto.Description;
            existingItem.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<List<Question>> GetAllAsync(int? caseStudyId)
        {
            List<Question> items;
            if (caseStudyId != null)
            {
                items = await _context.Questions
                    .Where(x => x.DeletedOn == null && x.CaseStudyId == caseStudyId)
                    .ToListAsync();
            }
            else
            {
                items = await _context.Questions
                    .Where(x => x.DeletedOn == null)
                    .ToListAsync();
            }

            return items;
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            var item = await _context.Questions
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);

            return item;
        }

        public async Task DeleteByCaseStudyIdAsync(int id)
        {
            var items = await _context.Questions
            .Where(x => x.CaseStudyId == id && x.DeletedOn == null)
            .ToListAsync();

            foreach (var item in items)
            {
                item.DeletedOn = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}