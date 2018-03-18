﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.ViewModels;
using Mapster;
using TestMakerFreeWebApp.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private ApplicationDbContext _context;

        #region Constructor

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region RESTful conventions methods

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = _context.Quizzes.Where(q => q.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID {id} was not found"
                });
            }

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented});
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            // retrieve the quiz to edit
            var quiz = _context.Quizzes.Where(q => q.Id == model.Id).FirstOrDefault();

            // handle invalid quizzes
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = $"Quiz {model.Id} was not found"
                });
            }

            // Update all the fields
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            // Set the update
            quiz.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return new JsonResult(
                quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                }
            );
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody] QuizViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            // handle the insert 
            var quiz = new Quiz();

            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            quiz.CreatedDate = DateTime.Now;
            quiz.LastModifiedDate = quiz.CreatedDate;

            // Temp author
            quiz.UserId = _context.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;

            // Add new quiz
            _context.Quizzes.Add(quiz);

            _context.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented});

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quiz = _context.Quizzes.Where(q => q.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new {Error = $"Quiz {id} was not found"});
            }

            _context.Quizzes.Remove(quiz);

            _context.SaveChanges();

            return new OkResult();
        }

        #endregion

        #region Attribute-based routing methods

        // GET: api/quiz/latest
        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = _context.Quizzes.OrderByDescending(q => q.CreatedDate)
                .Take(num)
                .ToArray();

            // output the result in JSON format
            return new JsonResult(
                latest.Adapt<QuizViewModel[]>(),
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
            var byTitle = _context.Quizzes.OrderBy(q => q.Title).Take(num).ToArray();

            return new JsonResult(
                byTitle.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings {Formatting = Formatting.Indented}
            );
        }

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var random = _context.Quizzes.OrderBy(q => Guid.NewGuid()).Take(num).ToArray();

            return new JsonResult(
                random.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }
        
        #endregion
    }
}
