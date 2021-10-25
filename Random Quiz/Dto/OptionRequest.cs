using RandomQuiz.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto
{
    public class OptionRequest
    {
        public string OptionText { get; set; }

        public static ICollection<OptionRequest> CreateCollection(ICollection<Option> options)
        {
            ICollection<OptionRequest> createdOptions = new List<OptionRequest>();
            foreach (var option in options)
            {
                createdOptions.Add(new OptionRequest { OptionText = option.OptionText });
            }
            return createdOptions;
        }
    }
}
