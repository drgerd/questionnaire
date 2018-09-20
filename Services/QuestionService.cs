using System.Collections.Generic;
using System.Linq;
using _questionnaire.Models.DB;
using _questionnaire.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace _questionnaire.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUserService _userService;
        private readonly QuestionnaireContext _questionnaireContext;

        public QuestionService(QuestionnaireContext questionnaireContext, IUserService userService)
        {
            this._questionnaireContext = questionnaireContext;
            this._userService = userService;
        }

        //ToDo: add authentication
        public StageResult SendAnswers(int stage, User user, IEnumerable<Answer> answers)
        {
            var dbUser = _userService.GetUserWithQuesitons(user).First();
            var stageQuestions = dbUser.UserToQuestionLinks.Where(x => x.StageNumber == stage).Select(x => x.Question);
            var FEQuestionsForStage = answers.GroupBy(x => x.QuestionId).Select(x => x.FirstOrDefault());

            if (dbUser.DoneStages + 1 != stage || stageQuestions.Count() != FEQuestionsForStage.Count())
            {
                throw new System.ArgumentException("Are you hacker or what >_< ? Stop playing with Stages!");
            }

            var questionsAnswered = 0;
            foreach (var question in stageQuestions)
            {
                var correctIds = question.Answers.Where(x => x.IsCorrectAnswer).Select(x => x.Id);
                questionsAnswered = FEQuestionsForStage.Where(x => x.QuestionId == question.Id).All(x => correctIds.Any(y => y == x.Id)) ? questionsAnswered++ : questionsAnswered;
            }

            var userToQuestionLink = dbUser.UserToQuestionLinks.First(x => x.StageNumber == stage);
            userToQuestionLink.IsComplete = true;
            userToQuestionLink.IsFailed = questionsAnswered == stageQuestions.Count();
            _questionnaireContext.UserToQuestionLinks.Update(userToQuestionLink);

            _questionnaireContext.SaveChanges();

            return GetStageResult(user);
        }

        public StageResult GetStageResult(User user)
        {
            var dbUser = _userService.GetUserWithQuesitons(user).First(); //todo:  check if that necessary

            var stageResult = new StageResult()
            {
                UserId = dbUser.Id
            };

            stageResult.Stage1Total = dbUser.UserToQuestionLinks.Count(x => x.StageNumber == 1);
            stageResult.Stage2Total = dbUser.UserToQuestionLinks.Count(x => x.StageNumber == 2);
            stageResult.Stage3Total = dbUser.UserToQuestionLinks.Count(x => x.StageNumber == 3);

            stageResult.Stage1DoneCount = dbUser.UserToQuestionLinks.Where(x => x.IsComplete && x.StageNumber == 1 && x.IsFailed == false).Count();
            stageResult.Stage2DoneCount = dbUser.UserToQuestionLinks.Where(x => x.IsComplete && x.StageNumber == 2 && x.IsFailed == false).Count();
            stageResult.Stage3DoneCount = dbUser.UserToQuestionLinks.Where(x => x.IsComplete && x.StageNumber == 3 && x.IsFailed == false).Count();

            return stageResult;
        }
    }
}