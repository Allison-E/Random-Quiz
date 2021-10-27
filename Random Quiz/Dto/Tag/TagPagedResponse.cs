using System;
using System.Collections.Generic;
using models = RandomQuiz.Db.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto.Tag
{
    public class TagPagedResponse
    {
        /// <summary>
        /// Creates a <see cref="PagedResponse{TagResponse}"/> from a <see cref="PagedResponse{models.Tag}"/>
        /// </summary>
        /// <param name="pagedResponse">A <see cref="PagedResponse{Question}"/>.</param>
        /// <returns>The created <see cref="PagedResponse{QuestionRequest}"/>.</returns>
        public static PagedResponse<TagResponse> Create(PagedResponse<models.Tag> pagedResponse)
        {
            PagedResponse<TagResponse> paged = new();

            paged.PageSize = pagedResponse.PageSize;
            paged.CurrentPageNumber = pagedResponse.CurrentPageNumber;
            paged.PageCount = pagedResponse.PageCount;
            paged.TotalQuestionCount = pagedResponse.TotalQuestionCount;

            foreach (var tag in pagedResponse.Response)
            {
                paged.Response.Add(TagResponse.Create(tag));
            }

            return paged;
        }
    }
}
