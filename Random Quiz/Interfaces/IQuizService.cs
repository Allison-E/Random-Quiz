using RandomQuiz.Dto;
using System;
using RandomQuiz.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomQuiz.Dto.Tag;

namespace RandomQuiz.Interfaces
{
    public interface IQuizService
    {
        /// <summary>
        /// Seeds the database and returns a <see cref="bool"/> representing the success of the operation.
        /// </summary>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if not.</returns>
        public Task<bool> SetupSeedData();
        /// <summary>
        /// Gets a random question.
        /// </summary>
        /// <returns>The picked <see cref="QuestionRequest"/>.</returns>
        public Task<QuestionRequest> GetRandomQuestionAsync();
        /// <summary>
        /// Gets a question by its ID.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The question with <paramref name="id"/> as its ID.</returns>
        public Task<QuestionRequest> GetQuestionByIdAsync(Guid id);
        public Task<object> GetQuestions(string? tag, int? pageSize, int? pageNumber);
    }
}
