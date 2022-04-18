using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class StoryReplyLikes : BaseEntity<int>
    {
        public int ReplyId { get; set; }
        public string UserId { get; set; }
    }
}
