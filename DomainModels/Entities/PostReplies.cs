using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class PostReplies : BaseEntityWithDates<int>
    {
        public User User { get; set; }
        public User Parent { get; set; }
        public Post Post { get; set; }
        public string Text { get; set; }
        public List<ReplyLikes> PostLikes { get; set; }
    }
}
