using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto
{
    public class PagedResponse<T> where T: class
    {
        private const int MAX_PAGE_SIZE = 100;
        private const int DEFAULT_PAGE_SIZE = 10;
        /// <summary>
        /// The number of elements that can be returned in a single response.
        /// </summary>
        public int PageSize
        {
            get => DEFAULT_PAGE_SIZE;  // Default value.
            set
            {
                if (value > MAX_PAGE_SIZE)
                    value = MAX_PAGE_SIZE;
                if (value < 1)
                    value = DEFAULT_PAGE_SIZE;
            }
        }
        /// <summary>
        /// The current page number.
        /// </summary>
        public int CurrentPageNumber { get; set; }
        public int PageCount { get; set; }
        public int TotalQuestionCount { get; set; }
        public List<T> Response { get; set; }

        public PagedResponse()
        {
            Response = new List<T>();
        }

        ~PagedResponse()
        {
            GC.Collect();
        }
    }
}
