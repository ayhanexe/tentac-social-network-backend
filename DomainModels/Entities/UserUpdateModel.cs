using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserUpdateModel
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
