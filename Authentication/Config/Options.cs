using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Config
{
    public static class Options
    {
        public static int MinimumUsernameLength { get; set; } = 5;
        public static int MaximumUsernameLength { get; set; } = 12;
        public static int MinimumPasswordLength { get; set; } = 8;
        public static int MaximumPasswordLength { get; set; } = 50;
    }
}
