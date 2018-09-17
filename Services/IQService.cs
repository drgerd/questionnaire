using System.Collections.Generic;

namespace _questionnaire.Services
{
    public interface IQService
    {
        void GetQuestions(int stage, int limit, IDictionary<int,int> levels);
    }
}