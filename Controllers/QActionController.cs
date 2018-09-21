using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _questionnaire.Models.DB;
using _questionnaire.Models.DTO;
using _questionnaire.Services;
using Microsoft.AspNetCore.Mvc;

namespace _questionnaire.Controllers
{
    [Route("api/[controller]")]
    public class QActionController : Controller
    {
        private IQuestionService _questionService;

        public QActionController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        [HttpPost("[action]")]
        public IActionResult SendAnswers([FromBody] SentAnswers sentAnswers)
        {
            return Json(this._questionService.SendAnswers(sentAnswers.StageNumber, sentAnswers.User, sentAnswers.Answers));
        }
    }
}
