using Random_Quiz.Dto;
using System;
using Random_Quiz.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Random_Quiz.Dto.Tag;

namespace Random_Quiz.Interfaces
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
    }
}
