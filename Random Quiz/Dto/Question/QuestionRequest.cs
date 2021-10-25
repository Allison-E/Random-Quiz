using Random_Quiz.Db.Models;
using System;
using System.Collections.Generic;

namespace Random_Quiz.Dto
{
    public class QuestionRequest
    {
        public Guid QuestionId { get; set; }
        public string Prompt { get; set; }
        public ICollection<OptionRequest> Options { get; set; }
        public string Answer { get; set; }
        public ICollection<TagRequest> Tags { get; set; }

        public static QuestionRequest Create(Question question)
        {
            return new QuestionRequest
            {
                QuestionId = question.QuestionId,
                Prompt = question.Prompt,
                Options = OptionRequest.CreateCollection(question.Options),
                Answer = question.Answer,
                Tags = TagRequest.CreateCollection(question.Tags)
            };
        }
    }
}
