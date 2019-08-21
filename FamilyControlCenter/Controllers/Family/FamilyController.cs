using FamilyControlCenter.Manager;
using FamilyControlCenter.Viewmodels.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Controllers
{
    public class FamilyController : Controller
    {
        FccManager Mgr { get; set; }

        //[Authorize]
        public ActionResult Person()
        {
            var vm = new PersonViewModel();
            var result = Mgr.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            var result = Mgr.HandleAction(vm);
            return View(vm);
        }
    }
}