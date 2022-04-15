using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserFriendRequests : BaseEntity<Guid>
    {
        public User User { get; set; }
        public User FriendRequestedUser { get; set; }
    }
}
