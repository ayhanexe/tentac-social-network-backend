using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Dtos
{
    public class UserFriendRequestDto
    {
        public string UserId { get; set; }
        public string FriendRequestedUserId { get; set; }
    }
}
