using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = RandomQuiz.Db.Models;

namespace RandomQuiz.Dto.Question
{
    public static class QuestionPagedResponse
    {
        /// <summary>
        /// Creates a <see cref="PagedResponse{QuestionRequest}"/> from a <see cref="PagedResponse{Question}"/>.
        /// </summary>
        /// <param name="pagedResponse">A <see cref="PagedResponse{Question}"/>.</param>
        /// <returns>The created <see cref="PagedResponse{QuestionRequest}"/>.</returns>
        public static PagedResponse<QuestionResponse> Create(PagedResponse<models.Question> pagedResponse)
        {
            PagedResponse<QuestionResponse> paged = new();

            paged.PageSize = pagedResponse.PageSize;
            paged.CurrentPageNumber = pagedResponse.CurrentPageNumber;
            paged.PageCount = pagedResponse.PageCount;
            paged.TotalQuestionCount = pagedResponse.TotalQuestionCount;
            
            foreach (var question in pagedResponse.Response)
            {
                paged.Response.Add(QuestionResponse.Create(question));
            }

            return paged;
        }
    }
}
