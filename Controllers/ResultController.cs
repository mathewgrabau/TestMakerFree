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
    public class ResultController : Controller
    {
        private ApplicationDbContext _context;

        public ResultController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region REST

        public IActionResult Get(int id)
        {
            var result = _context.Results.Where(r => r.Id == id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new { Error = $"Result {id} not found" });
            }

            return new JsonResult(
                result.Adapt<ResultViewModel>(),
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        [HttpPut]
        public IActionResult Put([FromBody] ResultViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var result = model.Adapt<Result>();

            result.CreatedDate = result.LastModifiedDate = DateTime.Now;

            _context.Results.Add(result);
            _context.SaveChanges();

            return new JsonResult(
                result.Adapt<ResultViewModel>(),
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ResultViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var result = _context.Results.Where(r => r.Id == model.Id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new { Error = $"Result {model.Id} not found" });
            }

            result.QuizId = model.QuizId;
            result.Text = model.Text;
            result.MinValue = model.MinValue;
            result.MaxValue = model.MaxValue;
            result.Notes = model.Notes;

            result.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return new JsonResult(
                result.Adapt<AnswerViewModel>(),
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Results.Where(r => r.Id == id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new {Error = $"Result {id} not found"});
            }

            _context.Results.Remove(result);
            _context.SaveChanges();

            return new OkResult();
        }

        #endregion


        // GET: api/result/all
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var results = _context.Results.Where(r => r.QuizId == quizId).ToArray();

            return new JsonResult(
                results.Adapt<ResultViewModel[]>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }
    }
}
