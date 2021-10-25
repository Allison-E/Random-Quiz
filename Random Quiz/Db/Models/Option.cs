using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Random_Quiz.Db.Models
{
    [Table("Options")]
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int QuestionId { get; set; }
    }
}
