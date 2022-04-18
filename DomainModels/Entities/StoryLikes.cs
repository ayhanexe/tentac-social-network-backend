using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class StoryLikes : BaseEntity<int>
    {
        public int StoryId { get; set; }
        public string UserId { get; set; }
    }
}
