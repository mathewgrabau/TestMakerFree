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
    public class ResultController : Controller
    {
        // GET: api/result/all
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<ResultViewModel>();

            // Just add some sample questions for right now
            for (var i = 0; i < 5; ++i)
            {
                sampleQuestions.Add(new ResultViewModel
                {
                    Id = i + 1,
                    QuizId = quizId,
                    Text = $"Sample Result {i + 1}",
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
