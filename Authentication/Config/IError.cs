using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Config
{
    public interface IError
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
