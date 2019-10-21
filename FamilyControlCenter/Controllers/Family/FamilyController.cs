using Shared.Interfaces.Managers;
using Shared.Viewmodels;
using Shared.Models;
using System.Linq;
using System.Web.Mvc;
using Shared.Enums;
using System;
using Shared.Interfaces.ViewBuilders;

namespace FamilyControlCenter.Controllers
{
    [RoutePrefix("{lang}/family")]
    public class FamilyController : BaseController
    {
        IFccViewBuilder _vwbFcc;

        public FamilyController(IFccViewBuilder vwbFcc)
        {
            _vwbFcc = vwbFcc;
        }

        //[Authorize]
        public ActionResult Person()
        {
            BeforeLoadAction();
            var vm = new PersonViewModel();
            _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            BeforeLoadAction();
            _vwbFcc.HandleAction(vm);
            return View(vm);
        }
    }
}