using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAFcc.Helpers
{
    public static class Globals
    {
        public static string ApiRoute { get { return @"http://localhost:55057/{lang}/api/"; } }
        public static string DefaultRoute { get { return @"http://localhost:55424/"; } }
    }
}
