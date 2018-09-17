using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _questionnaire.Models;
using _questionnaire.Services;
using Microsoft.AspNetCore.Mvc;

namespace _questionnaire.Controllers
{
    [Route("api/[controller]")]
    public class QActionController : Controller
    {
        private IQService _qService;

        public QActionController(IQService qService)
        {
            this._qService = qService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Question> WeatherForecasts()
        {
            return null;
        }
    }
}
