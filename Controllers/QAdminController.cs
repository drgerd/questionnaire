using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _questionnaire.Models;
using _questionnaire.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace _questionnaire.Controllers
{
    [Route("api/[controller]")]
    public class QAdminController : Controller
    {
        private readonly QuestionnaireContext _questionnaireContext;

        public QAdminController(QuestionnaireContext questionnaireContext)
        {
            this._questionnaireContext = questionnaireContext;
        }

        [HttpGet("[action]")]
        public void UpadateDB()
        {
            TextReader reader = new StreamReader("SEC2018_questionnaire.csv");
            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<CSVQuestionAnswerLine>().Where(x => !string.IsNullOrWhiteSpace(x.Question));

            var questions = records.Select(x =>
            {
                var question = new Question
                {
                    Description = x.Question,
                    Level = int.Parse(x.Level),
                    Answers = new List<Answer>()
                };

                var correctAnswers = x.RightAnswers.Split(';').Where(y => !string.IsNullOrWhiteSpace(y)).Select(y => int.Parse(y));

                for (var i = 1; i <= CSVQuestionAnswerLine.ANSWER_AMOUNT; i++)
                {
                    var val = (string)x.GetType().GetProperty($"Answer{i}").GetValue(x);
                    if (string.IsNullOrWhiteSpace(val)) break;

                    question.Answers.Add(new Answer
                    {
                        Description = val,
                        IsCorrectAnswer = correctAnswers.Any(y => y == i)
                    });
                }

                return question;

            });
            _questionnaireContext.Database.EnsureDeleted();
            _questionnaireContext.Database.EnsureCreated();
            _questionnaireContext.SaveChanges();

            foreach (var poc_q in questions)
            {
                _questionnaireContext.Add(poc_q);
            }
            _questionnaireContext.SaveChanges();

        }
    }
}
