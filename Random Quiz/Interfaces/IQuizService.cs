using RandomQuiz.Dto;
using System;
using RandomQuiz.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomQuiz.Dto.Tag;
using RandomQuiz.Dto.Question;

namespace RandomQuiz.Interfaces
{
    public interface IQuizService
    {
        /// <summary>
        /// Seeds the database and returns a <see cref="bool"/> representing the success of the operation.
        /// </summary>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if not.</returns>
        public Task<bool> SetupSeedDataAsync();
        /// <summary>
        /// Gets a random question.
        /// </summary>
        /// <returns>The picked <see cref="QuestionRequest"/>.</returns>
        public Task<QuestionResponse> GetRandomQuestionAsync();
        /// <summary>
        /// Gets a question by its ID.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The question with <paramref name="id"/> as its ID.</returns>
        public Task<QuestionResponse> GetQuestionByIdAsync(Guid id);
        /// <summary>
        /// Gets a collection of questions. Can be filtered by tags and paginated.
        /// </summary>
        /// <param name="tag">A tag filter.</param>
        /// <param name="pageSize">The number of questions returned in a request.</param>
        /// <param name="pageNumber">The page number of the current request.</param>
        /// <returns></returns>
        public Task<object> GetQuestionsAsync(string? tag, int? pageSize, int? pageNumber);

        public Task<string> CreateQuestionAsync(CreateQuestionRequest request);
    }
}
