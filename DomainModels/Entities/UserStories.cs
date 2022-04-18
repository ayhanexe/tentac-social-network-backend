using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserStories : BaseEntity<int>
    {
        public string UserId { get; set; }
        public int StoryId { get; set; }
    }
}
