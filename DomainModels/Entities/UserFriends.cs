using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserFriends : BaseEntity<int>
    {
        public User User { get; set; }
        public User Friend { get; set; }
    }
}
