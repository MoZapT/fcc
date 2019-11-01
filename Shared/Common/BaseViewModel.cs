using Shared.Enums;
using Shared.Viewmodels;

namespace Shared.Common
{
    public class BaseViewModel
    {
        public ActionCommand Command { get; set; }
        public VmState State { get; set; }

        public NavigationViewModel Navigation { get; set; }

        public PagingViewModel Paging { get; set; }

        public int Page { get; set; }
        public int Skip { get { return (Page - 1) * Take; } }
        public int Take { get; set; }
    }
}