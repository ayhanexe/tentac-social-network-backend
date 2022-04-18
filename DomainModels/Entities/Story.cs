using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class Story : BaseEntity<int>
    {
        public string Image { get; set; }
        public string Text { get; set; }
    }
}
