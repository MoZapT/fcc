using Shared.Interfaces.Common;
using FamilyControlCenter.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyControlCenter.Common
{
    public class BaseViewModel
    {
        public FccManager MgrFcc { get; set; }
        public IBaseModel Model { get; set; }
        public IEnumerable<IBaseModel> Models { get; set; }
        public bool IsListMode { get; set; }
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }

        public BaseViewModel()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            MgrFcc = new FccManager();
            EventArgument = "add";
            IsListMode = false;
        }

        public virtual void HandleAction()
        {
            switch (EventCommand)
            {
                case "list":
                    break;
                case "add":
                    break;
                case "edit":
                    break;
                case "delete":
                    break;
                default:
                    break;
            }
        }
    }
}