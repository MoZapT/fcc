using Shared.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyControlCenter.Common
{
    public class BaseViewModel
    {
        public string EscapeRoute { get; set; }
        public string TargetRoute { get; set; }

        public virtual void Initialize()
        {

        }
    }
}