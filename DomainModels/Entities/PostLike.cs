using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class PostLike
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public PostReplies PostReply { get; set; }
    }
}
