using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto.Question
{
    public class CreateQuestionRequest
    {
        [Required]
        public string Prompt { get; set; }
        [Required]
        public ICollection<string> Options { get; set; }
        [Required]
        public string Answer { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
