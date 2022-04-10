using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class ReplyLikes : BaseEntityWithDates<int>
    {
        public PostReplies PostReply { get; set; }
        public User User { get; set; }
    }
}
