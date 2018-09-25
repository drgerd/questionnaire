using System.Collections.Generic;
using _questionnaire.Models.DB;

namespace _questionnaire.Models.DTO
{
    public class SentAnswers
    {
        public User User { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public int StageNumber { get; set; }
    }
}