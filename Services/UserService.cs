using System;
using System.Collections.Generic;
using System.Linq;
using _questionnaire.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace _questionnaire.Services
{
    public class UserService : IUserService
    {
        private readonly QuestionnaireContext _questionnaireContext;

        public UserService(QuestionnaireContext questionnaireContext)
        {
            this._questionnaireContext = questionnaireContext;
        }

        private IEnumerable<Question> GenerateUserQuestions(User user)
        {
            var setting = _questionnaireContext.Settings.Include(x => x.LevelsForStages).First();
            var questions = _questionnaireContext.Questions.AsEnumerable();

            foreach (var stage in setting.LevelsForStages.OrderBy(x => x.StageNumber))
            {
                var questionsStageSet = new HashSet<long>();
                for (var i = 1; i <= Setting.AMOUNT_OG_STAGES; i++)
                {
                    var lvlSet = new HashSet<long>();
                    var lvlQuestions = questions.Where(x => x.Level == i).ToArray() ?? throw new IndexOutOfRangeException($"No questions with:{i} Level");
                    var rnd = new Random();
                    var passLoop = 500; // variable to prevent random generation
                    while (true)
                    {
                        passLoop--;
                        if (passLoop < 0)
                        {
                            throw new OverflowException($"Not enough questions in Level {i}, Change settings or add more questions");
                        }

                        var rndQuestion = lvlQuestions[rnd.Next(0, lvlQuestions.Count() - 1)];

                        if (!lvlSet.Contains(rndQuestion.Id))
                        {
                            lvlSet.Add(rndQuestion.Id);
                            user.UserToQuestionLinks.Add(new UserToQuestionLink
                            {
                                User = user,
                                Question = rndQuestion,
                                StageNumber = stage.StageNumber
                            });
                        }
                    }
                }
            }

            _questionnaireContext.SaveChanges();
            return user.UserToQuestionLinks.Select(x => x.Question);
        }

        public IQueryable<User> GetUser(User user){
            return  _questionnaireContext.Users.Where(x => x.Email.ToLower() == user.Email.ToLower().Trim());
        }

        public IQueryable<User> GetUserWithQuesitons(User user, bool includeAnswers = false){
            var query = this.GetUser(user)
                       .Include(x => x.UserToQuestionLinks)
                       .ThenInclude(x => x.Question);                       
            
            if(includeAnswers)
            {
                return query.ThenInclude(x=>x.Answers);
            }

            return query;
        }

        public IEnumerable<Question> SignUpAndOrGenerate(User user)
        {
           var dbUser = this.GetUserWithQuesitons(user).First();

            if (dbUser != null)
            {
                ///ToDO: Login Magic
                return dbUser.UserToQuestionLinks.Select(x=>x.Question);
            }

            return this.GenerateUserQuestions(user);
        }
    }
}