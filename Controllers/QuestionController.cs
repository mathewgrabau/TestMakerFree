using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Data.Models;
using TestMakerFreeWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region REST

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = _context.Questions.Where(q => q.Id == id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new {Error = $"Question {id} not found"});
            }

            return new JsonResult(
                question.Adapt<QuestionViewModel>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented});
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public IActionResult Put([FromBody]QuestionViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var question = model.Adapt<Question>();

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;
            question.CreatedDate = question.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return new JsonResult(
                question.Adapt<QuestionViewModel>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented});
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]QuestionViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var question = _context.Questions.Where(q => q.Id == model.Id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new {Error = $"Question {model.Id} was not found"});
            }

            // Update fields
            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;

            question.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return new JsonResult(
                question.Adapt<QuestionViewModel>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented});
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Grab recor
            var question = _context.Questions.Where(q => q.Id == id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new {Error = $"Question {id} not found"});
            }

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return new OkResult();
        }

        #endregion

        // GET: api/question/all
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var questions = _context.Questions.Where(q => q.QuizId == quizId).ToArray();

            return new JsonResult(
                questions.Adapt<QuestionViewModel[]>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }
    }
}
