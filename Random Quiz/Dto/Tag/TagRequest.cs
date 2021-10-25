using Random_Quiz.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Quiz.Dto
{
    public class TagRequest
    {
        public string TagId { get; set; }

        public static TagRequest Create(Db.Models.Tag tag) => new TagRequest { TagId = tag.TagId };

        public static ICollection<TagRequest> CreateCollection(ICollection<Db.Models.Tag> tags)
        {
            ICollection<TagRequest> createdTags = new List<TagRequest>();
            foreach (var tag in tags)
            {
                createdTags.Add(Create(tag));
            }
            return createdTags;
        }
    }
}
