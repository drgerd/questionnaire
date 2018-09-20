using System.Collections.Generic;
using _questionnaire.Models.DB;
using _questionnaire.Models.DTO;

namespace _questionnaire.Services
{
    public interface IQuestionService
    {
        StageResult SendAnswers(int stage, User user, IEnumerable<Answer> answers);
        StageResult GetStageResult(User user);
    }
}