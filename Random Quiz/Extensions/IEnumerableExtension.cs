using RandomQuiz.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RandomQuiz.Extensions
{
    public static class IEnumerableExtension
    {
        public static IQueryable<Question> FilterByTag(this IQueryable<Question> query, string tag)
        {
            return query.Where(x => x.Tags.Any(y => y.TagId.ToLower() == tag.ToLower())).OrderBy(x => x.Id);
        }
    }
}
