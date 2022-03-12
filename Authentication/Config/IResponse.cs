using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Config
{

    public interface IResponse
    {
        public List<IError> Messages { get; set; }
        public bool HasError { get; set; }
    }
}
