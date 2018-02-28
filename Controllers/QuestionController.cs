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
    public class QuestionController : Controller
    {
        // GET: api/question/all
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>();

            for (var i = 0; i < 5; ++i)
            {
                sampleQuestions.Add(new QuestionViewModel
                {
                    Id = i + 1,
                    QuizId = quizId,
                    Text = $"Sample Question {i + 1}",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(
                sampleQuestions,
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
