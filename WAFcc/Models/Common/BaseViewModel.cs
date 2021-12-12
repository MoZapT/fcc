using WAFcc.Enums;

namespace WAFcc.Models
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