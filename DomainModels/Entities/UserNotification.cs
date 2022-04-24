using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserNotification : BaseEntity<int>
    {
        public User User { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
    }
}
