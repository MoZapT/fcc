using Shared.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyControlCenter.Common
{
    public class BaseViewModel
    {
        public ActionCommand Command { get; set; }
    }

    public enum ActionCommand
    {
        List,
        Create,
        Edit,
        Add,
        Update,
        Delete,
        Back,
    }
}