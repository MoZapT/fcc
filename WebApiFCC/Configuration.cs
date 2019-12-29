using Data.Manager;
using Shared.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFCC
{
    public class Configuration
    {
        public IFccManager FccManager { get; set; }

        public Configuration()
        {
            FccManager = new FccManager();
        }
    }

    public static class ManagerCollection
    {
        public static Configuration Configuration { get; set; }
    }
}