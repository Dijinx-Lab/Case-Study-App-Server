using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Answers;
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

        [HttpPost("answer")]
        public async Task<IActionResult> SubmitAnswer([FromBody] AnswerRequestDto requestDto)
        {
            var team = await _teamRepo.GetByCodeAsync(requestDto.Code);
            if (team == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);
            int caseStudyId;
            List<Answer> createdAnswers = [];
            foreach (var item in requestDto.Answers)
            {
                var question = await _questionRepo.GetByIdAsync(item.QuestionId);
                if (question == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);
                caseStudyId = question.CaseStudyId;
                var createdAnswer = item.ToAnswerFromRequestDto();
                createdAnswer.TeamId = team.Id;
                createdAnswers.Add(createdAnswer);
            }
            var submittedAnswers = await _answerRepo.GetAllByTeamIdAsync(team.Id);

            foreach (var answer in submittedAnswers)
            {
                createdAnswers.RemoveAll(x => x.QuestionId == answer.QuestionId);
            }

            createdAnswers = await _answerRepo.CreateBatchAsync(createdAnswers);

            return ResponseUtility.ReturnOk(new { answers = createdAnswers });
        }
    }
}