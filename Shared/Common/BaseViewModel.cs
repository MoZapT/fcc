using Shared.Enums;

namespace Shared.Common
{
    public class BaseViewModel
    {
        public ActionCommand Command { get; set; }
        public VmState State { get; set; }
    }
}