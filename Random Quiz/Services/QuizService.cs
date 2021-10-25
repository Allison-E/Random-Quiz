using RandomQuiz.Interfaces;
using System;
using System.Collections.Generic;
using conc = System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RandomQuiz.Db;
using RandomQuiz.Db.Models;
using RandomQuiz.Dto;
using RandomQuiz.Dto.Tag;
using RandomQuiz.Util;

namespace RandomQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly QuestionContext context;

        public QuizService(QuestionContext context)
        {
            this.context = context;
        }

        public async Task<bool> SetupSeedData()
        {
            var question = new Question
            {
                QuestionId = System.Guid.NewGuid(),
                Prompt = "What is the name of the person who developed this API?",
                Options = new List<Option>
                    {
                        new Option { OptionText = "Emmanuel" },
                        new Option { OptionText = "Allison" },
                        new Option { OptionText = "Isobonye" },
                        new Option { OptionText = "Michael" }
                    },
                Answer = "Emmanuel",
                Tags = new List<Tag>
                    {
                        new Tag{ TagId = "Name"},
                        new Tag{ TagId = "API" }
                    }
            };
            await context.Questions.AddAsync(question);

            question = new Question
            {
                QuestionId = System.Guid.NewGuid(),
                Prompt = "On what weekday did I start this project?",
                Options = new List<Option>
                    {
                        new Option{ OptionText = "Saturday" },
                        new Option{ OptionText = "Monday" },
                        new Option{ OptionText = "Wednesday" },
                        new Option{ OptionText = "Friday" }
                    },
                Answer = "Friday",
                Tags = new List<Tag>
                    {
                        new Tag{ TagId = "Day"}
                    }
            };
            await context.Questions.AddAsync(question);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<QuestionRequest> GetRandomQuestionAsync()
        {
            int upperBound = noOfQuestionEntries().Result;
            int seed = RandomGen.Generate(1, upperBound);
            var question = await context.Questions
                .Include(x => x.Options)
                .Include(x => x.Tags)
                .Where(q => q.Id == seed)
                .FirstAsync();

            return QuestionRequest.Create(question);
        }

        public async Task<QuestionRequest> GetQuestionByIdAsync(Guid id)
        {
            var question = await context.Questions
                .Include(x => x.Options)
                .Include(x => x.Tags)
                .Where(x => x.QuestionId == id)
                .FirstAsync();

            return QuestionRequest.Create(question);
        }

        private async Task<int> noOfQuestionEntries()
        {
            return await context.Questions.CountAsync();
        }

        public async Task<List<QuestionRequest>> GetQuestions(string? tag, int? pageSize)
        {
            List<QuestionRequest> questions = new List<QuestionRequest>();
            if (pageSize == null)
                pageSize = 10;

            if (tag == null)
            {
                for (int i = 0; i < pageSize; i++)
                {
                    questions.Add(await GetRandomQuestionAsync());
                }
            }
            else
            {
                
            }
            return questions;
        }

        private IEnumerable<Question> filterQuestionsByTag(string tag, IEnumerable<Question> query)
        {
            return query.Where(x => x.Tags.Any(y => y.TagId == tag));
        }
    }
}
