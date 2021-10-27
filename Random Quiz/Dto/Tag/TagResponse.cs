using RandomQuiz.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuiz.Dto
{
    public class TagResponse
    {
        public string TagId { get; set; }

        public static TagResponse Create(Db.Models.Tag tag) => new() { TagId = tag.TagId };

        public static ICollection<TagResponse> CreateCollection(ICollection<Db.Models.Tag> tags)
        {
            ICollection<TagResponse> createdTags = new List<TagResponse>();
            foreach (var tag in tags)
            {
                createdTags.Add(Create(tag));
            }
            return createdTags;
        }
    }
}
