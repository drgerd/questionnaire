using System.Collections.Generic;
using _questionnaire.Models;

namespace _questionnaire.Services
{
    public class QService : IQService
    {
        public QuestionnaireContext _questionnaireContext { get; }

        public QService(QuestionnaireContext questionnaireContext){
            _questionnaireContext = questionnaireContext;
        }

        public void GetQuestions(int stage, int limit, IDictionary<int, int> levels)
        {
            throw new System.NotImplementedException();
        }
    }
}