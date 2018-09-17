using System.Collections.Generic;

namespace _questionnaire.Models
{
    public class Question
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public ICollection<Answer> Answers { get; set; }

        //public ICollection<Answer> CorrectAnswers{get;set;}
        //public ICollection<CorrectQuestionToAnswer> CorrectAnswers { get; }

    }

    public class Answer
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsCorrectAnswer { get; set; }
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