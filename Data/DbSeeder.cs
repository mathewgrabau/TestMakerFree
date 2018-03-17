using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Data.Models;

namespace TestMakerFreeWebApp.Data
{
    
    public class DbSeeder
    {
        #region Public Methods

        public static void Seed(ApplicationDbContext dbContext)
        { 
            // Create default users if there are none
            if (!dbContext.Users.Any())
            {
                CreateUsers(dbContext);
            }

            // Create default quizzes if there are none, including the set of questions and answers.
            if (!dbContext.Quizzes.Any())
            {
                CreateQuizzes(dbContext);
            }
        }

        #endregion

        #region Seed Methods

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            var createdDate = new DateTime(2017, 03, 14, 22, 00, 00);
            var lastModifiedDate = DateTime.Now;

            // Create the admin user account (if it doesn't exist already)
            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            dbContext.Users.Add(adminUser);

#if DEBUG

            // Create sample user accounts
            var userOne = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "UserOne",
                Email = "userone@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var userTwo = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "UserTwo",
                Email = "userone@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var userThree = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "UserThree",
                Email = "userthree@testmakerfree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            dbContext.Users.AddRange(userOne, userTwo, userThree);
#endif

            dbContext.SaveChanges();
        }

        private static void CreateQuizzes(ApplicationDbContext dbContext)
        {
            var createdDate = new DateTime(2018, 03, 14, 22, 00, 00);
            var lastModifiedDate = DateTime.Now;

            // grab the admin user for making the quizzes
            var authorId = dbContext.Users.Where(u => u.UserName == "Admin")
                .FirstOrDefault().Id;

#if DEBUG
            // create some sample quizzes
            const int numToCreate = 55;

            for (var i = 0; i < numToCreate; ++i)
            {
                CreateSampleQuiz(dbContext, i, authorId, numToCreate - i, 3, 3, 3, createdDate.AddDays(-numToCreate));
            }
#endif

            // Create more quizzes with better descriptive data (we'll add the questions, answers and results later on)
            EntityEntry<Quiz> entryOne = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Are you more Light or Dark side of the Force?",
                Description = "Star Wars personality test",
                Text = @"Choose wisely you must, young padawan: " +
                       "this test will prove if your will is strong enough " +
                       "to adhere to the principles of the light side of the force " +
                       "or if you're fated to embrace the dark side. " +
                       "If you want to become a true Jedi, you can't possibly miss this!",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Quiz> entryTwo = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "GenX, GenY or Genz?",
                Description = "Find out what decade most represents you",
                Text = @"Do you feel confortable in your generation? " +
                       "What year should you have been born in?" +
                       "Here's a bunch of questions that will help you to find " +
                       "out!",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Quiz> entryThree = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Attack On Titan personality test",
                Text = @"Do you relentlessly seek revenge like Eren? " +
                    "Are you willing to put your like on the stake to " +
                    "protect your friends like Mikasa ? " +
                    "Would you trust your fighting skills like Levi " +
                    "or rely on your strategies and tactics like Arwin? " +
                    "Unveil your true self with this Attack On Titan " +
                    "personality test!",
                ViewCount = 5200,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });


            // Persist changes
            dbContext.SaveChanges();
        }
        #endregion

        #region Utility Methods

        private static void CreateSampleQuiz(ApplicationDbContext dbContext,
            int num,
            string authorId,
            int viewCount,
            int numberOfQuestions,
            int numberOfAnswersPerQuestion,
            int numberOfResults,
            DateTime createdDate)
        {
            var quiz = new Quiz()
            {
                UserId = authorId,
                CreatedDate = createdDate,
                Title = $"Quiz {num} Title",
                Description = $"This is a sample description for quiz {num}.",
                Text = "This is a sample quiz created by the DbSeeder class for testing purposes. Contains auto-generated as well.",
                ViewCount = viewCount,
                LastModifiedDate = createdDate
            };

            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();

            for (var i = 0; i < numberOfQuestions; ++i)
            {
                var question = new Question
                {
                    QuizId = quiz.Id,
                    Text = "Sample question created with DbSeeder. All child answers are generated too.",
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                };
                dbContext.Questions.Add(question);
                dbContext.SaveChanges();

                for (var j = 0; j < numberOfAnswersPerQuestion; ++j)
                {
                    var answer = dbContext.Answers.Add(new Answer
                    {
                        QuestionId = question.Id,
                        Text = "Sample answer created by DbSeeder",
                        Value = j,
                        CreatedDate = createdDate,
                        LastModifiedDate = createdDate
                    });
                }
            }

            for (var i = 0; i < numberOfResults; ++i)
            {
                dbContext.Results.Add(new Result()
                {
                    QuizId = quiz.Id,
                    Text = "Sample result from DbSeeder",
                    MinValue = 0,
                    // max value should be equal to answers number * max answer 
                    MaxValue = numberOfAnswersPerQuestion * 2,
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                });
            }

            dbContext.SaveChanges();
        }

        #endregion
    }
}
