using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Questions;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Mappers;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.User
{
    [ApiController]
    [Route(RouteConstants.UserQuestionRoute)]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly ITeamRepository _teamRepo;
        private readonly ICaseStudyRepository _studyRepo;
        private readonly IAnswerRepository _answerRepo;
        public QuestionController(
            IQuestionRepository questionRepo, ITeamRepository teamRepo, ICaseStudyRepository studyRepo, IAnswerRepository answerRepo)
        {
            _questionRepo = questionRepo;
            _teamRepo = teamRepo;
            _studyRepo = studyRepo;
            _answerRepo = answerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByTeamAndCaseStudy([FromQuery] string code, [FromQuery] int id)
        {
            var team = await _teamRepo.GetByCodeAsync(code);
            if (team == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            bool caseStudyExists = await _studyRepo.CheckExistsAsync(id);
            if (!caseStudyExists) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            var items = await _questionRepo.GetAllAsync(id);
            var teamAnswers = await _answerRepo.GetAllByTeamIdAsync(team.Id);
            List<QuestionAnswerResponseDto> questionWithAnswers = [];
            foreach (var item in items)
            {
                var questionWithAnswer = item.ToResponseWithAnswer();
                Answer? answer = teamAnswers.FirstOrDefault(x => x.QuestionId == item.Id);
                questionWithAnswer.Answer = answer;
                questionWithAnswers.Add(questionWithAnswer);
            }
            return ResponseUtility.ReturnOk(new { questions = questionWithAnswers });
        }
    }
}