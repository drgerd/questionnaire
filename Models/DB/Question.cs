using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace _questionnaire.Models.DB
{
    public class Question
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public ICollection<Answer> Answers { get; set; }
        
        [NotMapped]
        public int StageNumber { get; set; }

        [JsonIgnore]
        public ICollection<UserToQuestionLink> UserToQuestionLinks { get; set; }

        //public ICollection<Answer> CorrectAnswers{get;set;}
        //public ICollection<CorrectQuestionToAnswer> CorrectAnswers { get; }

    }

    public class Answer
    {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public bool IsCorrectAnswer { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
        //public ICollection<CorrectQuestionToAnswer> Original { get; }

    }

    // public class CorrectQuestionToAnswer
    // {
    //     public long QuestionId { get; set; }
    //     public Question Question { get; set; }

    //     public long AnswerId { get; set; }
    //     public Answer Answer { get; set; }
    // }
}