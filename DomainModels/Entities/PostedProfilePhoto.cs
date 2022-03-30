using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class PostedProfilePhoto
    {
        public int lastModified { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string webkitRelativePath { get; set; }
    }
}
