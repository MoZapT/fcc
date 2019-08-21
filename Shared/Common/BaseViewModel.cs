using Shared.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Common
{
    public class BaseViewModel
    {
        public ActionCommand Command { get; set; }
        public VmState State { get; set; }
    }

    public enum ActionCommand
    {
        None,
        Create,
        Edit,
        Add,
        Update,
        Delete,
    }

    public enum VmState
    {
        List,
        Detail,
    }
}