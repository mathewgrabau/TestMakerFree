using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using TestMakerFreeWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        // GET: api/quiz/latest
        [HttpGet("Latest/{num}")]
        public IActionResult Latest(int num = 10)
        {
            var sampleQuizzes = new List<QuizViewModel>();

            sampleQuizzes.Add(new QuizViewModel
            {
                Id = 1,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Anime-related personality test",
                CreatedDate =  DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            // add some additional quizzes
            for (var i = 2; i <= num; ++i)
            {
                sampleQuizzes.Add(new QuizViewModel
                {
                    Id = i,
                    Title = $"Sample Quiz {i}",
                    Description = "This is a sample quiz",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            // output the result in JSON format
            return new JsonResult(
                sampleQuizzes,
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var dataset = ((JsonResult) Latest(num)).Value as List<QuizViewModel>;

            return new JsonResult(
                dataset.OrderBy(q => q.Title),
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value
                as List<QuizViewModel>;

            return new JsonResult(
                sampleQuizzes.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

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
