using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomQuiz.Db.Models
{
    [Table("Options")]
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int QuestionId { get; set; }
    }
}
