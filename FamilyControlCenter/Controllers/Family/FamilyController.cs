using Shared.Interfaces.Managers;
using Shared.Viewmodels;
using Shared.Models;
using System.Linq;
using System.Web.Mvc;
using Shared.Enums;
using System;

namespace FamilyControlCenter.Controllers
{
    [RoutePrefix("{lang}/family")]
    public class FamilyController : BaseController
    {
        IFccManager _mgrFcc;

        public FamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        //[Authorize]
        public ActionResult Person()
        {
            BeforeLoadAction();
            var vm = new PersonViewModel();
            _mgrFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            BeforeLoadAction();
            _mgrFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public PartialViewResult LoadPersonRelationsList(string personId)
        {
            BeforeLoadAction();

            var vm = new PersonViewModel
            {
                Relations = _mgrFcc.GetPersonRelationGroupsByPersonId(personId).ToList()
            };
            return PartialView("Person/_RelationsList", vm);
        }
    }
}