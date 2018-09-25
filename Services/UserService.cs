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

                for (var i = 1; i <= Setting.AMOUNT_OF_LEVELS; i++)
                {

                    // HollyShit code goes here >_<
                    int levelCount = 0;
                    switch (i)
                    {
                        case 1 when (stage.Lvl1Count >= 0):
                            levelCount = stage.Lvl1Count.Value;
                            break;

                        case 2 when (stage.Lvl2Count >= 0):
                            levelCount = stage.Lvl2Count.Value;
                            break;

                        case 3 when (stage.Lvl3Count >= 0):
                            levelCount = stage.Lvl3Count.Value;
                            break;

                        case 4 when (stage.Lvl4Count >= 0):
                            levelCount = stage.Lvl4Count.Value;
                            break;

                        default:
                            continue;
                    }

                    var lvlSet = new HashSet<long>();
                    var lvlQuestions = questions.Where(x => x.Level == i).ToArray() ?? throw new IndexOutOfRangeException($"No questions with:{i} Level");
                    var rnd = new Random();
                    var passLoop = 500; // variable to prevent random generation
                    while (true)
                    {
                        if (lvlSet.Count() == levelCount)
                        {
                            break;
                        }

                        passLoop--;
                        if (passLoop < 0)
                        {
                            throw new OverflowException($"Not enough questions in Level {i}, Change settings or add more questions");
                        }

                        var rndQuestion = lvlQuestions[rnd.Next(0, lvlQuestions.Count())];

                        if (!lvlSet.Contains(rndQuestion.Id) && !user.UserToQuestionLinks.Any(x=>x.QuestionId == rndQuestion.Id))
                        {
                            lvlSet.Add(rndQuestion.Id);
                            user.UserToQuestionLinks.Add(new UserToQuestionLink
                            {
                                User = user,
                                UserId = user.Id,
                                Question = rndQuestion,
                                QuestionId = rndQuestion.Id,
                                StageNumber = stage.StageNumber
                            });
                        }
                    }
                }
            }

            _questionnaireContext.SaveChanges();
            var dbuser = this.GetUserWithQuesitons(user,true).First();
            return dbuser.UserToQuestionLinks.Select(x=>x.Question);
        }

        public IQueryable<User> GetUser(User user)
        {
            return _questionnaireContext.Users.Where(x => x.Email.ToLower() == user.Email.ToLower().Trim());
        }

        public IQueryable<User> GetUserWithQuesitons(User user, bool includeAnswers = false)
        {
            var query = this.GetUser(user)
                       .Include(x => x.UserToQuestionLinks)
                       .ThenInclude(x => x.Question);

            if (includeAnswers)
            {
                return query.ThenInclude(x => x.Answers);
            }

            return query;
        }

        public IEnumerable<Question> SignUpAndOrGenerate(User user)
        {

            void assignStageNumber(List<Question> questions)
            {
                questions.ForEach(x=>x.StageNumber = x.UserToQuestionLinks.First().StageNumber);
            }

            var dbUser = this.GetUserWithQuesitons(user, true).FirstOrDefault();

            if (dbUser != null)
            {
                ///ToDO: Login Magic
                var existQuestions = dbUser.UserToQuestionLinks.Select(x => x.Question).ToList();
                assignStageNumber(existQuestions);
                return existQuestions;
            }

            _questionnaireContext.Add(new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UniqKey = Guid.NewGuid()
            });
            _questionnaireContext.SaveChanges();

            var newQuestions = this.GenerateUserQuestions(this.GetUserWithQuesitons(user, true).First()).ToList();
            assignStageNumber(newQuestions);
            return newQuestions;
        }
    }
}