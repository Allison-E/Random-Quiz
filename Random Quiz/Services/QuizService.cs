using Microsoft.EntityFrameworkCore;
using RandomQuiz.Db;
using RandomQuiz.Db.Models;
using RandomQuiz.Dto;
using RandomQuiz.Dto.Question;
using RandomQuiz.Extensions;
using RandomQuiz.Interfaces;
using RandomQuiz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly QuestionContext context;

        public QuizService(QuestionContext context)
        {
            this.context = context;
        }

        public async Task<bool> SetupSeedDataAsync()
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

        public async Task<QuestionResponse> GetRandomQuestionAsync()
        {
            int upperBound = await context.Questions.CountAsync();
            int seed = RandomGen.Generate(1, upperBound);
            var question = await context.Questions
                .Include(x => x.Options)
                .Include(x => x.Tags)
                .Where(q => q.Id == seed)
                .FirstAsync();

            return QuestionResponse.Create(question);
        }

        public async Task<QuestionResponse> GetQuestionByIdAsync(Guid id)
        {
            var question = await context.Questions
                .Include(x => x.Options)
                .Include(x => x.Tags)
                .Where(x => x.QuestionId == id)
                .FirstAsync();

            return QuestionResponse.Create(question);
        }

        public async Task<object> GetQuestionsAsync(string? tag, int? pageSize, int? pageNumber)
        {
            pageSize = (pageSize == null) ? 10 : pageSize;
            pageNumber = (pageNumber == null) ? 1 : pageNumber;

            if (tag == null)
            {
                return getRandomQuestionsAsync((int)pageSize);
            }
            else
            {
                return await getPaginatedQuestionsAsync(tag, (int)pageSize, (int)pageNumber);
            }
        }

        public async Task<string> CreateQuestionAsync(CreateQuestionRequest request)
        {
            var question = createQuestionFromRequest(request);
            await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();

            return question.QuestionId.ToString();
        }

        private async Task<PagedResponse<QuestionResponse>> getPaginatedQuestionsAsync(string tag, int pageSize, int pageNumber)
        {
            var response = await context.Questions
                .Include(x => x.Options)
                .Include(x => x.Tags)
                .FilterByTag(tag)
                .PaginateAsync(pageSize, pageNumber);

            return QuestionPagedResponse.Create(response);
        }

        private async Task<List<QuestionResponse>> getRandomQuestionsAsync(int pageSize)
        {
            List<QuestionResponse> questions = new();
            int totalQuestionCount = await context.Questions.CountAsync();

            for (int i = 0; i < ((totalQuestionCount < pageSize) ? totalQuestionCount : pageSize); i++)
            {
                questions.Add(await GetRandomQuestionAsync());
            }

            return questions;
        }

        private Question createQuestionFromRequest(CreateQuestionRequest request)
        {
            Question question = new()
            {
                QuestionId = Guid.NewGuid(),
                Prompt = request.Prompt,
                Answer = request.Answer,
                Options = new List<Option>(),
                Tags = new List<Tag>()
            };

            foreach (var option in request.Options)
            {
                question.Options.Add(new() { OptionText = option });
            }

            foreach (var tag in request.Tags)
            {
                question.Tags.Add(new() { TagId = tag });
            }

            return question;
        }


    }
}
