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
    public class AnswerController : BaseApiController
    {
        public AnswerController(ApplicationDbContext context) : base(context)
        {
        }

        #region REST

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = Context.Answers.Where(a => a.Id == id).FirstOrDefault();

            if (answer == null)
            {
                return NotFound(new {Error = $"Answer {id} not found"});
            }

            return new JsonResult(
                answer.Adapt<AnswerViewModel>(),
                JsonSettings);
        }

        [HttpPut]
        public IActionResult Put([FromBody] AnswerViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var answer = model.Adapt<Answer>();

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;
            answer.Value = model.Value;
            answer.CreatedDate = answer.LastModifiedDate = DateTime.Now;

            Context.Answers.Add(answer);
            Context.SaveChanges();

            return new JsonResult(
                answer.Adapt<AnswerViewModel>(),
                JsonSettings);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AnswerViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var answer = Context.Answers.Where(a => a.Id == model.Id).FirstOrDefault();

            if (answer == null)
            {
                return NotFound(new {Error = $"Answer {model.Id} not found"});
            }

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;
            answer.Value = model.Value;
            answer.CreatedDate = answer.LastModifiedDate = DateTime.Now;

            Context.SaveChanges();

            return new JsonResult(
                answer.Adapt<AnswerViewModel>(),
                JsonSettings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var answer = Context.Answers.Where(a => a.Id == id).FirstOrDefault();

            if (answer == null)
            {
                return NotFound(new {Error = $"Answer {id} not found"});
            }

            Context.Answers.Remove(answer);
            Context.SaveChanges();

            return new OkResult();
        }

        #endregion


        // GET: api/answer/all
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var answers = Context.Answers.Where(a => a.QuestionId == questionId).ToArray();

            return new JsonResult(
                answers.Adapt<AnswerViewModel[]>(),
                JsonSettings
            );
        }
    }
}
