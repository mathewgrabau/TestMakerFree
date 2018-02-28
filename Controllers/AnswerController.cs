using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        // GET: api/answer/all
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleQuestions = new List<AnswerViewModel>();

            for (var i = 0; i < 5; ++i)
            {
                sampleQuestions.Add(new AnswerViewModel
                {
                    Id = i + 1,
                    QuestionId = questionId,
                    Text = $"Sample Answer {i + 1}",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(
                sampleQuestions,
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }
    }
}
