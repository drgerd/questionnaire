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
            var dbUser = _userService.GetUserWithQuesitons(user, true).First();
            var UserToQuestionLinks = dbUser.UserToQuestionLinks.ToList();
            var stageQuestions = dbUser.UserToQuestionLinks.Where(x => x.StageNumber == stage).Select(x => x.Question);
            var FEQuestionsForStage = answers.GroupBy(x => x.QuestionId).Select(x => x.FirstOrDefault());

            if (dbUser.DoneStages + 1 != stage
            || UserToQuestionLinks.Any(x => x.IsFailed)
            || UserToQuestionLinks.Where(x => x.StageNumber == stage).All(x => x.IsComplete))
            {
                throw new System.ArgumentException("Are you hacker or what >_< ? Stop playing with Stages!");
            }

            var questionsAnswered = 0;
            foreach (var question in stageQuestions)
            {
                var correctIds = question.Answers.Where(x => x.IsCorrectAnswer).Select(x => x.Id);
                var isAnswered  = answers.Where(x => x.QuestionId == question.Id).All(x => correctIds.Any(y => y == x.Id));
                if(isAnswered){
                    questionsAnswered++;
                } 

                var userToQuestionLink = dbUser.UserToQuestionLinks.Where(x => x.StageNumber == stage && x.QuestionId == question.Id).First();
                 userToQuestionLink.IsComplete = true;
                 userToQuestionLink.IsFailed = !isAnswered;
                 _questionnaireContext.UserToQuestionLinks.Update(userToQuestionLink);
            }

            dbUser.DoneStages++;
            _questionnaireContext.Users.Update(dbUser);

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

            stageResult.CurrentStage = dbUser.DoneStages;

            bool isWin(int stageTotal, int stageDoneCount)
            {
                return stageTotal == stageDoneCount;
            }

            switch (stageResult.CurrentStage)
            {
                case 1:
                    stageResult.IsWin = isWin(stageResult.Stage1Total, stageResult.Stage1DoneCount);
                    break;
                case 2:
                    stageResult.IsWin = isWin(stageResult.Stage2Total, stageResult.Stage2DoneCount);
                    break;
                case 3:
                    stageResult.IsWin = isWin(stageResult.Stage3Total, stageResult.Stage3DoneCount);
                    break;
            }

            return stageResult;
        }
    }
}