using RandomQuiz.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto
{
    public class OptionResponse
    {
        public string OptionText { get; set; }

        public static ICollection<OptionResponse> CreateCollection(ICollection<Option> options)
        {
            ICollection<OptionResponse> createdOptions = new List<OptionResponse>();
            foreach (var option in options)
            {
                createdOptions.Add(new OptionResponse { OptionText = option.OptionText });
            }
            return createdOptions;
        }
    }
}
