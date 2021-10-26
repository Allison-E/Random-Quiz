using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomQuiz.Db.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public Guid QuestionId { get; set; }
        /// <summary>
        /// The question to be answered.
        /// </summary>
        public string Prompt { get; set; }
        public ICollection<Option> Options { get; set; }
        public string Answer { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
