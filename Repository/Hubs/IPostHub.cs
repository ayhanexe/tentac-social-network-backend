using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Hubs
{
    public interface IPostHub
    {
        Task SendMessage(string message);
    }
}
