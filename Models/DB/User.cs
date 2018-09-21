using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace _questionnaire.Models.DB
{
    public class User
    {
        public long Id { get; set; }
        public Guid UniqKey { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long DoneStages { get; set; }
        [JsonIgnore]
        public ICollection<UserToQuestionLink> UserToQuestionLinks { get; set; }
    }

    public class UserToQuestionLink
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long QuestionId { get; set; }
        public Question Question { get; set; }
        public int StageNumber { get; set; }
        public bool IsFailed { get; set; }
        public bool IsComplete { get; set; }
    }
}