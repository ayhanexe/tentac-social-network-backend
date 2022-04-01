using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class Post: BaseEntityWithDates<int>
    {
        public string Text { get; set; }
        public User User { get; set; }
    }
}
