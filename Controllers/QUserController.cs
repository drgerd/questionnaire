using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _questionnaire.Models.DB;
using _questionnaire.Services;
using Microsoft.AspNetCore.Mvc;

namespace _questionnaire.Controllers
{
    [Route("api/[controller]")]
    public class QUserController : Controller
    {
        private IUserService _userService;

        public QUserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("[action]")]
        public IActionResult SignUp(User user)
        {
            var validateResult = TryValidateModel(user);
            if (!validateResult)
            {
                return new BadRequestResult();
            }

            var questions = _userService.SignUpAndOrGenerate(user);
            return new JsonResult(questions);
        }
    }
}
