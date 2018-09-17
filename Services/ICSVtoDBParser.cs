using System.Collections.Generic;
using _questionnaire.Models;

namespace _questionnaire.Services
{
    public interface ICSVtoDBParser
    {
        ICollection<Question> ParseQuestions();
        
    }



}