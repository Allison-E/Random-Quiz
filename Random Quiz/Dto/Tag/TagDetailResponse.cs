using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto.Tag
{
    public class TagDetailResponse
    {
        public string TagId { get; set; }
        public int QuestionCount { get; set; }

        public static TagDetailResponse ToTagDetailResponse(Db.Models.Tag tag)
        {
            if (tag != null)
                return new TagDetailResponse
                {
                    TagId = tag.TagId,
                    QuestionCount = tag.Questions.Count
                };
            return null;
        }
    }
}
