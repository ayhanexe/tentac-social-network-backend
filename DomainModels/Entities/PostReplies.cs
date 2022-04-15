using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class PostReplies : BaseEntityWithDates<int>
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public User Parent { get; set; }
        public Post Post { get; set; }
        public string Text { get; set; }
        public List<ReplyLikes> PostLikes { get; set; }
    }
}
