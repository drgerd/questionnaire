using System.Collections.Generic;
using System.Linq;
using _questionnaire.Models.DB;

namespace _questionnaire.Services
{
    public interface IUserService
    {
        //void SignUp(User user);
        IEnumerable<Question> SignUpAndOrGenerate(User user);
        IQueryable<User> GetUserWithQuesitons(User user,  bool includeAnswers = false);
    }
}