using models = RandomQuiz.Db.Models;
using System;
using System.Collections.Generic;

namespace RandomQuiz.Dto
{
    public class QuestionRequest
    {
        public Guid QuestionId { get; set; }
        public string Prompt { get; set; }
        public ICollection<OptionRequest> Options { get; set; }
        public string Answer { get; set; }
        public ICollection<TagRequest> Tags { get; set; }

        /// <summary>
        /// Creates a <see cref="QuestionRequest"/> from a <see cref="models.Question"/>.
        /// </summary>
        /// <param name="question">A <see cref="models.Question"/>.</param>
        /// <returns>The created <see cref="QuestionRequest"/>.</returns>
        public static QuestionRequest Create(models.Question question)
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
