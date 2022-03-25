using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserWall : BaseEntityWithDates<int>
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string Photo { get; set; }
    }
}
