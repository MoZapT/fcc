using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Viewmodels;
using System.Collections.Generic;

namespace WebAppFcc.Shared.Common
{
    public class BaseViewModel
    {
        public ActionCommand Command { get; set; }
        public VmState State { get; set; }

        public List<string> NavigationLog { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
    }
}