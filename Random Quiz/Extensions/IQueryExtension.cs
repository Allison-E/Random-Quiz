using RandomQuiz.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using RandomQuiz.Db.Models;

namespace RandomQuiz.Extensions
{
    public static class IQueryExtension
    {
        public static async Task<PagedResponse<T>> PaginateAsync<T>(this IQueryable<T> query, int pageSize, int pageNumber) where T: class
        {
            var paged = new PagedResponse<T>();
            pageNumber = (pageNumber < 0) ? 1 : pageNumber;
            //
            // Todo: Handle the error of the pageNumber being higher than the total number of questions.
            //
            paged.CurrentPageNumber = pageNumber;
            paged.PageSize = pageSize;
            paged.TotalQuestionCount = await query.CountAsync();

            var pageCount = (double)paged.TotalQuestionCount / pageSize;
            paged.PageCount = (int)Math.Ceiling(pageCount);

            var startRow = (pageNumber - 1) * pageSize;
            paged.Response = await query
                .Skip(startRow)
                .Take(pageSize)
                .ToListAsync();

            return paged;
        }
    }
}
