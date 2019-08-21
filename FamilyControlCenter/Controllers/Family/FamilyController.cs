using Shared.Interfaces.Managers;
using Shared.Viewmodels.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Controllers
{
    public class FamilyController : Controller
    {
        IFccManager _mgrFcc;

        public FamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        //[Authorize]
        public ActionResult Person()
        {
            var vm = new PersonViewModel();
            var result = _mgrFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            var result = _mgrFcc.HandleAction(vm);
            ModelState.Clear();
            return View(vm);
        }
    }
}