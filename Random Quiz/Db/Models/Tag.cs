using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomQuiz.Db.Models
{
    [Table("Tags")]
    public class Tag
    {
        /// <summary>
        /// The text of the tag. Since every tag's text would be unique, this would
        /// also be used as the key of the Tag table in the database.
        /// </summary>
        public string TagId { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
